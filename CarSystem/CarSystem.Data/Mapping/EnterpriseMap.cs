using CarSystem.Data.Mapping.Helpers;
using CarSystem.Domain;
using FluentNHibernate.Mapping;

namespace CarSystem.Data.Mapping
{
    public class EnterpriseMap : ClassMapBase<Enterprise>
    {
        public EnterpriseMap()
        {
            Table("TBEnterprises");

            Map(x => x.CompanyName);
            Map(x => x.Cnpj);

            HasMany(x => x.Vehicles).AsSet().Access.CamelCaseField(Prefix.Underscore).Cascade.AllDeleteOrphan();

        }
    }
}
