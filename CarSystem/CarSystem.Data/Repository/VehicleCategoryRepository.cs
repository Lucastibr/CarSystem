using CarSystem.Domain;
using CarSystem.Domain.Repository;
using Codout.Framework.NH;
using NHibernate;

namespace CarSystem.Data.Repository
{
    public class VehicleCategoryRepository : NHRepository<VehicleCategory>, IVehicleCategoryRepository
    {
        public VehicleCategoryRepository(ISession session) : base(session)
        {
        }
    }
}