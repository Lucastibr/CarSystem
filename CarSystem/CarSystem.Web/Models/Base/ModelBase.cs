using System;
using CarSystem.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace  CarSystem.Models.Base
{
    public abstract class ModelBase : IModel
    {
        public Guid? Id { get; set; }

        public virtual void Bind(IUnitOfWorkCarSystem unitOfWork)
        {

        }

        public virtual bool IsValid(IUnitOfWorkCarSystem unitOfWork, ModelStateDictionary modelState)
        {
            return modelState.IsValid;
        }
    }
}