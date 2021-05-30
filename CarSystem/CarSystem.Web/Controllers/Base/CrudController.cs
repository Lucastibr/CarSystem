using System;
using AspNetCoreHero.ToastNotification.Abstractions;
using CarSystem.Controllers.Helpers;
using CarSystem.Domain;
using CarSystem.Models.Base;
using Codout.Framework.DAL.Entity;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CarSystem.Web.Controllers.Base
{
    public abstract class CrudController<TDomain, TModel> : CustomControllerBase
      where TDomain : class, IEntity<Guid?>
      where TModel : class, IModel, new()
    {
        protected CrudController(IWebHostEnvironment hostingEnvironment,
            IUnitOfWorkCarSystem unitOfWork,
            AppUserState appUserState)
            : base(hostingEnvironment, unitOfWork, appUserState)
        {
            
        }
        
        public virtual ActionResult BindGrid([ModelBinder(typeof(DataSourceRequestModelBinder))] DataSourceRequest request)
        {
            var itens = DataHandler(request);
            return Json(itens); 
        }

        #region Index
        public virtual ActionResult Index()
        {
            return View("_List");
        }

        #endregion

        #region Novo
        public virtual ActionResult New()
        {
            var model = new TModel();
            model.Bind(UnitOfWork);
            return View("_EditOrCreate", model);
        }

        [HttpPost]
        public virtual ActionResult New(TModel model)
        {
            if (model.IsValid(UnitOfWork, ModelState))
            {
                var domain = ToDomain(model);

                if (domain != null)
                {
                    if (model.IsValid(UnitOfWork, ModelState))
                    {
                        UnitOfWork.Repository<TDomain>().Save(domain);
                        UnitOfWork.SaveChanges();
                        SuccessMessage = "Registro salvo com sucesso!";
                        return RedirectToAction("Index");
                    }
                }
            }

            model.Bind(UnitOfWork);

            return View("_EditOrCreate", model);
        }
        #endregion

        #region Editar
        public virtual ActionResult Edit(Guid? id)
        {
            if (!id.HasValue)
                return NotFound();

            var domain = UnitOfWork.Repository<TDomain>().Get(id);
            var model = ToModel(domain);
            model.Bind(UnitOfWork);
            return View("_EditOrCreate", model);
        }

        [HttpPost]
        public virtual ActionResult Edit(TModel model)
        {
            if (model.IsValid(UnitOfWork, ModelState))
            {
                var domain = ToDomain(model);

                if (domain != null)
                {
                    UnitOfWork.SaveChanges();
                    SuccessMessage = "Registro salvo com sucesso!";
                    return RedirectToAction("Index");
                }
            }

            model.Bind(UnitOfWork);
            return View("_EditOrCreate", model);
        }
        #endregion

        #region Excluir
        [HttpPost]
        public virtual JsonResult Delete(Guid? id)
        {
            try
            {
                UnitOfWork.Repository<TDomain>().Delete(x => x.Id == id);

                UnitOfWork.SaveChanges();

                return Json(new { result = true });

            }
            catch (Exception ex)
            {
                return Json(new { result = false, message = ex.Message });
            }
        }
        #endregion

        public abstract TModel ToModel(TDomain domain);

        public abstract TDomain ToDomain(TModel model);

        public abstract DataSourceResult DataHandler(DataSourceRequest request);
    }
}
