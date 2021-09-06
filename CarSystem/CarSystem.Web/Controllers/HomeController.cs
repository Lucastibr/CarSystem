using System.Linq;
using CarSystem.Web.Controllers.Base;
using CarSystem.Controllers.Helpers;
using CarSystem.Domain;
using CarSystem.Web.Models.Home;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NHibernate.Mapping;

namespace CarSystem.Web.Controllers
{
    public class HomeController : CustomControllerBase
    {
        public HomeController(IWebHostEnvironment hostingEnvironment, IUnitOfWorkCarSystem unitOfWork, AppUserState appUserState) : base(hostingEnvironment, unitOfWork, appUserState)
        {
        }

        public IActionResult Index()
        {
            var model = new DashboardViewModel
            {
                Price = UnitOfWork.Vehicle.All()
                    .Select(x => new DashboardViewModel
                    {
                        Price = x.Price
                    }).Average(x => x.Price),
                SumVehicles = UnitOfWork.Vehicle.All().Count(),
                SumEnterprises = UnitOfWork.Enterprise.All().Count()
            };

            return View(model);
        }

        public JsonResult Chart()
        {
            var vehicles = UnitOfWork.Vehicle.All().Count();
            var enterprise = UnitOfWork.Enterprise.All().Count();
            

            return Json(new {vehicles, enterprise });
        }
    }
}
