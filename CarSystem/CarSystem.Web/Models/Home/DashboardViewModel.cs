using CarSystem.Models.Base;

namespace CarSystem.Web.Models.Home
{
    public class DashboardViewModel : ModelBase
    {
        public decimal? Price {get; init;}
        public int? SumVehicles {get; init;}
        public int? SumEnterprises { get; init; }
    }
}