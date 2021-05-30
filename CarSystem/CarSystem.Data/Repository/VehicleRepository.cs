using CarSystem.Domain;
using CarSystem.Domain.Repository;
using Codout.Framework.NH;
using NHibernate;

namespace CarSystem.Data.Repository
{
    public class VehicleRepository : NHRepository<Vehicle>, IVechileRepository
    {
        public VehicleRepository(ISession session) : base(session)
        {
            
        }
    }
}
