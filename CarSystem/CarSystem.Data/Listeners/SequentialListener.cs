using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Eventon.Core.Domain.Base;
using NHibernate.Event;
using NHibernate.Persister.Entity;

namespace CarSystem.Data.Listeners
{
    public class SequentialListener : IPreInsertEventListener
    {
        public async Task<bool> OnPreInsertAsync(PreInsertEvent @event, CancellationToken cancellationToken)
        {
            return await Task.Run(() => OnPreInsert(@event), cancellationToken);
        }

        public bool OnPreInsert(PreInsertEvent @event)
        {
            var sequentials = (from p in @event.Entity.GetType().GetProperties()
                               let attr = p.GetCustomAttribute<SequentialAttribute>()
                               where attr != null
                               select new { Property = p, Attribute = attr })
                .ToList();

            var propertyAndValues = GetPropertyValues(@event.Entity);

            if (!sequentials.Any())
                return false;

            var entityName = @event.Persister as SingleTableEntityPersister;

            if (entityName != null)
            {
                foreach (var sequential in sequentials)
                {
                    var sequentialColumnName = entityName.GetPropertyColumnNames(sequential.Property.Name).FirstOrDefault();

                    if (sequentialColumnName == null)
                        continue;

                    var groupByColumnNames = new List<string>();
                    var whereByValuesNames = new Dictionary<string, string>();

                    string groupBy = null;
                    string where = string.Empty;

                    if (sequential.Attribute.GroupByProperties.Any())
                    {
                        foreach (var propertyGroupBy in sequential.Attribute.GroupByProperties)
                        {
                            var nameOfProperty = entityName.GetPropertyColumnNames(propertyGroupBy).FirstOrDefault();
                            var valueOfProperty = GetPropertyValues(propertyAndValues.FirstOrDefault(p => p.Key == propertyGroupBy).Value).FirstOrDefault(p => p.Key == "Id").Value.ToString();

                            whereByValuesNames.Add(nameOfProperty, valueOfProperty);

                            groupByColumnNames.Add(nameOfProperty);
                        }

                        where = Where(whereByValuesNames);

                        groupBy = $"GROUP BY {string.Join(", ", groupByColumnNames)}";
                    }

                    var sql = $"SELECT ISNULL(MAX({sequentialColumnName}),0) FROM {entityName.TableName} {where} {groupBy}";

                    var nextSequential = @event.Session
                                       .CreateSQLQuery(sql)
                                       .UniqueResult<long>() + 1;

                    if (nextSequential < sequential.Attribute.InitialSequence)
                        nextSequential = nextSequential + sequential.Attribute.InitialSequence;

                    Set(@event.Persister, @event.State, sequential.Property.Name, nextSequential);

                    var propertyInfo = @event.Entity.GetType().GetProperty(sequential.Property.Name);
                    propertyInfo?.SetValue(@event.Entity, Convert.ChangeType(nextSequential, propertyInfo.PropertyType), null);
                }
            }

            return false;
        }

        private void Set(IEntityPersister persister, object[] state, string propertyName, object value)
        {
            var index = Array.IndexOf(persister.PropertyNames, propertyName);
            if (index == -1)
                return;
            state[index] = value;
        }

        private static string Where(Dictionary<string, string> whereByValuesNames)
        {
            string @where = string.Empty;

            var it = 0;
            foreach (var value in whereByValuesNames)
            {
                if (it == 0)
                {
                    @where += $"WHERE {value.Key} = '{value.Value}'";
                }
                else
                {
                    @where += $" AND  {value.Key} = '{value.Value}'";
                }

                it++;
            }
            return @where;
        }

        private static Dictionary<string, object> GetPropertyValues(object o)
        {
            Dictionary<string, object> propertyValues = new Dictionary<string, object>();
            Type ObjectType = o.GetType();
            PropertyInfo[] properties = ObjectType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                propertyValues.Add(property.Name, property.GetValue(o, null));
            }
            return propertyValues;
        }
    }
}
