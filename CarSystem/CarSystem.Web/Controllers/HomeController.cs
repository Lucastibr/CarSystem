using CarSystem.Web.Controllers.Base;
using CarSystem.Controllers.Helpers;
using CarSystem.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CarSystem.Web.Controllers
{
    public class HomeController : CustomControllerBase
    {
        public HomeController(IWebHostEnvironment hostingEnvironment, IUnitOfWorkCarSystem unitOfWork, AppUserState appUserState) : base(hostingEnvironment, unitOfWork, appUserState)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
