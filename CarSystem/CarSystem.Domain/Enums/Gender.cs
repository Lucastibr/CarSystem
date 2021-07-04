using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarSystem.Domain.Enums
{
    public enum Gender
    {
        [Description("Masculino")]
        [Display(Name = "Masculino")]
        Man,

        [Description("Feminino")]
        [Display(Name = "Feminino")]
        Woman,
    }
}
