using CarSystem.Controllers.Helpers;
using CarSystem.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CarSystem.Web.Controllers.Base
{
    public class CustomControllerBase : Controller
    {
        public CustomControllerBase(IWebHostEnvironment hostingEnvironment,
            IUnitOfWorkCarSystem unitOfWork,
            AppUserState appUserState)
        {
            HostingEnvironment = hostingEnvironment;
            UnitOfWork = unitOfWork;
            AppUserState = appUserState;
        }

        public AppUserState AppUserState { get; set; }

        public IWebHostEnvironment HostingEnvironment { get; }

        public IUnitOfWorkCarSystem UnitOfWork { get; }

        public string SuccessMessage
        {
            get => TempData["SuccessMessage"] as string;
            set => TempData["SuccessMessage"] = value;
        }
    }
}
