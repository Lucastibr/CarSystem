using CarSystem.Data.Mapping.Helpers;
using CarSystem.Domain;

namespace CarSystem.Data.Mapping
{
    public class VehicleMap : ClassMapBase<Vehicle>
    {
        public VehicleMap()
        {
            Table("TBVehicles");

            Map(x => x.CarLicensePlate);
            Map(x => x.Chassis);
            
            References(x =>x.Enterprise);

        }
    }
}
