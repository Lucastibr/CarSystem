using System.Linq;
using CarSystem.Controllers.Helpers;
using CarSystem.Domain;
using CarSystem.Web.Controllers.Base;
using CarSystem.Web.Models.Vehicle;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Hosting;

namespace CarSystem.Web.Controllers
{
    public class VehicleController : CrudController<Vehicle, VehicleViewModel>
    {
        public VehicleController(IWebHostEnvironment hostingEnvironment, IUnitOfWorkCarSystem unitOfWork, AppUserState appUserState) 
            : base(hostingEnvironment, unitOfWork, appUserState)
        {
        }

        public override VehicleViewModel ToModel(Vehicle domain)
        {
            var model = new VehicleViewModel
            {
                Id = domain.Id,
                CarLicensePlate = domain.CarLicensePlate,
                Chassis = domain.Chassis,
                EnterpriseId = domain.Enterprise?.Id
            };

            return model;
        }

        public override Vehicle ToDomain(VehicleViewModel model)
        {
            var domain = model.Id.HasValue ? UnitOfWork.Vehicle.Get(model.Id) : new Vehicle();
            domain.CarLicensePlate = model.CarLicensePlate;
            domain.Chassis = model.Chassis;
            domain.Enterprise = model.EnterpriseId.HasValue ? UnitOfWork.Enterprise.Get(model.EnterpriseId) : null;

            return domain;
        }

        public override DataSourceResult DataHandler(DataSourceRequest request)
        {
            var itens = UnitOfWork.Vehicle
                .All()
                .Select(x => new
                {
                    Id = x.Id,
                    CarLicensePlate = x.CarLicensePlate,
                    Chassis = x.Chassis,
                    Enterprise = x.Enterprise.CompanyName
                }).ToDataSourceResult(request, x => new VehicleViewModel
                {
                    Id = x.Id,
                    CarLicensePlate = x.CarLicensePlate,
                    Chassis = x.Chassis,
                    EnterpriseName = x.Enterprise
                });

            return itens;
        }
    }
}
