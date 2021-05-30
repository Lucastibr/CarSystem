using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using CarSystem.Domain;
using CarSystem.Models.Base;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CarSystem.Web.Models.Enterprise
{
    public class EnterpriseViewModel : ModelBase
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Nome da Empresa")]
        public string CompanyName {get; set;}

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "CNPJ da Empresa")]
        public string Cnpj {get; set;}

        public override bool IsValid(IUnitOfWorkCarSystem unitOfWork, ModelStateDictionary modelState)
        {
            if(unitOfWork.Enterprise.Find(x => x.Cnpj == Cnpj && x.Id != Id).Any())
                modelState.AddModelError("Cnpj","CNPJ já cadastrado, tente novamente com outro CNPJ");

            return base.IsValid(unitOfWork, modelState);
        }
    }
}
