using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarSystem.Domain.Enums
{
    public enum VehicleType
    {
        [Description("Carro")]
        [Display(Name = "Carro")]
        Car,

        [Description("Motocicleta")]
        [Display(Name = "Motocicleta")]
        MotorCycle,

        [Description("Caminhão")]
        [Display(Name = "Caminhão")]
        Truck,

        [Description("Ônibus")]
        [Display(Name = "Ônibus")]
        Bus,
    }
}
