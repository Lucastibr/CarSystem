using CarSystem.Domain;
using CarSystem.Domain.Repository;
using Codout.Framework.NH;
using NHibernate;

namespace CarSystem.Data.Repository
{
    public class EnterpriseRepository : NHRepository<Enterprise>, IEnterpriseRepository
    {
        public EnterpriseRepository(ISession session) : base(session)
        {
            
        }
    }
}
