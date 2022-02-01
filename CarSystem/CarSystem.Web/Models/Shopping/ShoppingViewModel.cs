using System;
using CarSystem.Domain;
using CarSystem.Models.Base;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarSystem.Web.Models.Shopping;

public class ShoppingViewModel : ModelBase
{

    public Guid? CategoryId { get; set; }

    public SelectList Categories {get; set;}
    
}