using CarSystem.Data.Repository;
using CarSystem.Domain;
using CarSystem.Domain.Repository;
using Codout.Framework.DAL;
using Codout.Framework.NH;

namespace CarSystem.Data
{
    public class UnitOfWork : NHUnitOfWork, IUnitOfWorkCarSystem
    {
        private readonly RepositoryFactory _factory;

        static UnitOfWork()
        {
            RepositoryFactory.RegisterRepository<IVechileRepository, VehicleRepository, Vehicle>();
            RepositoryFactory.RegisterRepository<IEnterpriseRepository, EnterpriseRepository, Enterprise>();
            RepositoryFactory.RegisterRepository<IVehicleCategoryRepository, VehicleCategoryRepository, VehicleCategory>();
        }

        public UnitOfWork(ITenant tenant = null) : base(tenant)
        {
            _factory = new RepositoryFactory(Session);
        }

        public IVechileRepository Vehicle  => _factory.Get<IVechileRepository>();
        public IEnterpriseRepository Enterprise => _factory.Get<IEnterpriseRepository>();
        public IVehicleCategoryRepository VehicleCategory => _factory.Get<IVehicleCategoryRepository>();
    }
}
