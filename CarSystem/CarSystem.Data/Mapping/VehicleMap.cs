using CarSystem.Data.Mapping.Helpers;
using CarSystem.Domain;

namespace CarSystem.Data.Mapping
{
    public class VehicleMap : ClassMapBase<Vehicle>
    {
        public VehicleMap()
        {
            Table("TBVehicles");

            Map(x => x.LicensePlate);
            Map(x => x.Chassis);
            Map(x => x.Price);
            Map(x => x.VehicleType);
            Map(x => x.YearRelease);
            Map(x => x.CarBody);
            Map(x => x.Image);
            
            References(x =>x.Enterprise);

        }
    }
}
