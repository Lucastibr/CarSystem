using System.Linq;
using CarSystem.Domain.Base;

namespace CarSystem.Domain
{
    public class Vehicle : Entity
    {
        /// <summary>
        /// Placa do Carro
        /// </summary>
        public virtual string CarLicensePlate {get; set;}

        /// <summary>
        /// Chassi do Carro
        /// </summary>
        public virtual string Chassis {get; set;}

        public virtual Enterprise Enterprise {get; set;}
    }
}
