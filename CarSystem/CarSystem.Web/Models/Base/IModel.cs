using System;
using CarSystem.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace  CarSystem.Models.Base
{
    public interface IModel
    {
        Guid? Id { get; set; }

        void Bind(IUnitOfWorkCarSystem unitOfWork);

        bool IsValid(IUnitOfWorkCarSystem unitOfWork, ModelStateDictionary modelState);
    }
}