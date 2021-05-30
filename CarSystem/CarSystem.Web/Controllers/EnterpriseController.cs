using System;
using System.Linq;
using AspNetCoreHero.ToastNotification.Abstractions;
using CarSystem.Web.Controllers.Base;
using CarSystem.Controllers.Helpers;
using CarSystem.Domain;
using CarSystem.Web.Models.Enterprise;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CarSystem.Web.Controllers
{
    public class EnterpriseController : CrudController<Enterprise, EnterpriseViewModel>
    {
        public EnterpriseController(IWebHostEnvironment hostEnvironment, IUnitOfWorkCarSystem unitOfWork, AppUserState userState)
            : base(hostEnvironment, unitOfWork, userState)
        {
        }
        public override EnterpriseViewModel ToModel(Enterprise domain)
        {
            var model = new EnterpriseViewModel
            {
                Id = domain.Id,
                Cnpj = domain.Cnpj,
                CompanyName = domain.CompanyName
            };

            return model;
        }

        public override Enterprise ToDomain(EnterpriseViewModel model)
        {
            var domain = model.Id.HasValue ? UnitOfWork.Enterprise.Get(model.Id) : new Enterprise();

            domain.CompanyName = model.CompanyName;
            domain.Cnpj = model.Cnpj;

            return domain;
        }

        [HttpPost]
        public override DataSourceResult DataHandler([DataSourceRequest] DataSourceRequest request)
        {
            var itens = UnitOfWork.Enterprise
                .All()
                .ToDataSourceResult(request, x => new EnterpriseViewModel
                {
                    Id = x.Id,
                    CompanyName = x.CompanyName,
                    Cnpj = x.Cnpj
                });

            return itens;
        }

    }
}
