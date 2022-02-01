using CarSystem.Data.Mapping.Helpers;
using CarSystem.Domain;

namespace CarSystem.Data.Mapping
{
    public class VehicleCategoryMap : ClassMapBase<VehicleCategory>
    {
        public VehicleCategoryMap()
        {
            Table("TBVehicleCategories");

            Map(x => x.Name);
        }
    }
}