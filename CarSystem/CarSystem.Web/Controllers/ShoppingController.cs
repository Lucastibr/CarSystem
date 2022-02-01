using System;
using CarSystem.Controllers.Helpers;
using CarSystem.Domain;
using CarSystem.Web.Controllers.Base;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CarSystem.Web.Controllers;

public class ShoppingController : CustomControllerBase
{
    public ShoppingController(IWebHostEnvironment hostingEnvironment, IUnitOfWorkCarSystem unitOfWork, AppUserState appUserState) : base(hostingEnvironment, unitOfWork, appUserState)
    {
    }

    public IActionResult Index()
    {
        throw new NotImplementedException();
    }
}