using System;
using System.Linq;
using CarSystem.Domain;
using CarSystem.Models.Base;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarSystem.Web.Models.Home
{
    public class DashboardViewModel : ModelBase
    {
        public decimal? Price { get; init; }
        public int? SumVehicles { get; init; }
        public int? SumEnterprises { get; init; }

        public Guid? VehicleCategoryId { get; init; }

        public SelectList VehicleCategories { get; set; }

        public override void Bind(IUnitOfWorkCarSystem unitOfWork)
        {
            VehicleCategories = new SelectList(unitOfWork.VehicleCategory.All().Select(x => new {x.Id, x.Name})
                .OrderBy(x => x.Name), "Id", "Name", VehicleCategoryId);
        }
    }
}