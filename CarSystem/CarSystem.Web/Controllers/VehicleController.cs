using System;
using System.IO;
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
        private readonly  IWebHostEnvironment _hostEnvironment;
        public VehicleController(IWebHostEnvironment hostingEnvironment, IUnitOfWorkCarSystem unitOfWork, AppUserState appUserState) 
            : base(hostingEnvironment, unitOfWork, appUserState)
        {
            _hostEnvironment = hostingEnvironment;
        }

        public override VehicleViewModel ToModel(Vehicle domain)
        {

            var model = new VehicleViewModel
            {
                Id = domain.Id,
                LicensePlate = domain.LicensePlate,
                Chassis = domain.Chassis,
                CarBody = domain.CarBody,
                VehicleType = domain.VehicleType,
                YearRelease = domain.YearRelease.Date,
                Price = domain.Price,
                EnterpriseId = domain.Enterprise?.Id,
                VehicleImage = domain.Image
            };

            return model;
        }

        public override Vehicle ToDomain(VehicleViewModel model)
        {
            string uniqueFileName = UploadedFile(model);  

            var domain = model.Id.HasValue ? UnitOfWork.Vehicle.Get(model.Id) : new Vehicle();
            domain.LicensePlate = model.LicensePlate;
            domain.Chassis = model.Chassis;
            domain.Price = model.Price;
            domain.VehicleType = model.VehicleType.GetValueOrDefault();
            domain.YearRelease = model.YearRelease;
            domain.CarBody = model.CarBody.GetValueOrDefault();
            domain.Enterprise = model.EnterpriseId.HasValue ? UnitOfWork.Enterprise.Get(model.EnterpriseId) : null;
            domain.Image = uniqueFileName;

            return domain;
        }

        public override DataSourceResult DataHandler(DataSourceRequest request)
        {
            var itens = UnitOfWork.Vehicle
                .All()
                .Select(x => new
                {
                    Id = x.Id,
                    CarLicensePlate = x.LicensePlate,
                    Chassis = x.Chassis,
                    Enterprise = x.Enterprise.CompanyName,
                    Price = x.Price,
                    VehicleType = x.VehicleType,
                    CarBody = x.CarBody,
                    YearRelease = x.YearRelease,
                    Image = x.Image
                }).ToDataSourceResult(request, x => new VehicleViewModel
                {
                    Id = x.Id,
                    LicensePlate = x.CarLicensePlate,
                    Chassis = x.Chassis,
                    EnterpriseName = x.Enterprise,
                    Price = x.Price,
                    VehicleType = x.VehicleType,
                    CarBody = x.CarBody,
                    YearRelease = x.YearRelease,
                    VehicleImage = x.Image
                });

            return itens;
        }

        private string UploadedFile(VehicleViewModel model)  
        {  
            string uniqueFileName = null;  
  
            if (model.Image != null)  
            {  
                string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "img");  
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;  
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);  
                using (var fileStream = new FileStream(filePath, FileMode.Create))  
                {  
                    model.Image.CopyTo(fileStream);  
                }  
            }  
            return uniqueFileName;  
        }  
    }
}
