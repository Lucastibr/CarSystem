using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CarSystem.Domain;
using CarSystem.Domain.Enums;
using CarSystem.Models.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarSystem.Web.Models.Vehicle
{
    public class VehicleViewModel : ModelBase
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Placa do Veículo")]
        public string LicensePlate {get; set;}

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Chassi do Carro")]
        public string Chassis {get; set;}

        public Guid? EnterpriseId {get; set;}
        public SelectList Enterprises {get; set;}

        public string EnterpriseName {get; set;}

        [Display(Name = "Tipo de Veículo")]
        public VehicleType? VehicleType {get; set;}

        [Display(Name = "Corpo do Veículo")]
        public CarBody? CarBody {get; set;}

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Ano de Lançamento")]
        public DateTime? YearRelease {get; set;}

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Valor do Veículo")]
        public decimal Price {get; set;}

        [Display(Name ="Imagem")]
        public IFormFile Image {get; init;}

        public string VehicleImage { get; set;}
        public override void Bind(IUnitOfWorkCarSystem unitOfWork)
        {
            Enterprises = new SelectList(unitOfWork.Enterprise.All().Select(x => new {x.Id, x.CompanyName})
               .OrderBy(x => x.CompanyName), "Id", "CompanyName", EnterpriseId);
        }

        public override bool IsValid(IUnitOfWorkCarSystem unitOfWork, ModelStateDictionary modelState)
        {
           
           if (unitOfWork.Vehicle.Find(x => x.LicensePlate == LicensePlate && x.Id != Id).Any())
                    modelState.AddModelError("CarLicensePlate", "Placa já cadastrada, tente novamente com outra Placa");

           if (unitOfWork.Vehicle.Find(x => x.Chassis == Chassis && x.Id != Id).Any())
                    modelState.AddModelError("Chassis", "Chassi já cadastrado, tente novamente com outro Chassi");

           if (LicensePlate.Length > 8 && LicensePlate != null)
                    modelState.AddModelError("LicensePlate", "Placa só deve ter 7 Caracteres");
            
           return base.IsValid(unitOfWork, modelState);
        }
    }
}
