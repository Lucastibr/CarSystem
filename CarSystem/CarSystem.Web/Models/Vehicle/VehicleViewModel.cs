using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CarSystem.Domain;
using CarSystem.Models.Base;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarSystem.Web.Models.Vehicle
{
    public class VehicleViewModel : ModelBase
    {
        [Required]
        [Display(Name = "Placa do Carro")]
        public string CarLicensePlate {get; set;}

        [Required]
        [Display(Name = "Chassi do Carro")]
        public string Chassis {get; set;}

        public Guid? EnterpriseId {get; set;}
        public SelectList Enterprises {get; set;}

        public string EnterpriseName {get; set;}

        public override void Bind(IUnitOfWorkCarSystem unitOfWork)
        {
            Enterprises = new SelectList(unitOfWork.Enterprise.All().Select(x => new {x.Id, x.CompanyName})
               .OrderBy(x => x.CompanyName), "Id", "CompanyName", EnterpriseId);
        }

        public override bool IsValid(IUnitOfWorkCarSystem unitOfWork, ModelStateDictionary modelState)
        {
            if(unitOfWork.Vehicle.Find(x => x.CarLicensePlate == CarLicensePlate && x.Id != Id).Any())
                modelState.AddModelError("CarLicensePlate","Placa já cadastrada, tente novamente com outra Placa");

            if(unitOfWork.Vehicle.Find(x => x.Chassis == Chassis && x.Id != Id).Any())
                modelState.AddModelError("Chassis","Chassi já cadastrado, tente novamente com outro Chassi");

            return base.IsValid(unitOfWork, modelState);
        }
    }
}
