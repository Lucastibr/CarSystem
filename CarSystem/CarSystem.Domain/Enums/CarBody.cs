using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarSystem.Domain.Enums
{
    public enum CarBody
    {
        [Description("SUV")]
        [Display(Name = "SUV")]
        Suv,

        [Description("Sedan")]
        [Display(Name = "Sedan")]
        Sedan,

        [Description("Hatch")]
        [Display(Name = "Hatch")]
        Hatch,

        [Description("Van")]
        [Display(Name = "Van")]
        Van,

        [Description("Pickup")]
        [Display(Name = "Pickup")]
        Pickup,
    }
}