using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codout.Framework.DAL.Entity;
using FluentNHibernate.Mapping;

namespace CarSystem.Data.Mapping.Helpers
{
    public class ClassMapBase<T> : ClassMap<T> where T : IEntity<Guid?>, IEntity
    {
        public ClassMapBase()
        {
            Id(x => x.Id).GeneratedBy.Guid();
        }
    }
}
