using System;
using System.Linq;
using CarSystem.Domain.Base;
using CarSystem.Domain.Enums;

namespace CarSystem.Domain
{
    public class Vehicle : Entity
    {
        /// <summary>
        /// Placa do Carro
        /// </summary>
        public virtual string LicensePlate {get; set;}

        /// <summary>
        /// Chassi do Carro
        /// </summary>
        public virtual string Chassis {get; set;}

        public virtual Enterprise Enterprise {get; set;}

        /// <summary>
        /// Preço do Veículo
        /// </summary>
        public virtual decimal Price {get; set;}
        
        /// <summary>
        /// Tipo de Veículo
        /// </summary>
        public virtual VehicleType? VehicleType {get; set;}

        /// <summary>
        /// Ano de Lançamento
        /// </summary>
        public virtual DateTime YearRelease {get; set;}

        /// <summary>
        /// Corpo do Carro
        /// </summary>
        public virtual CarBody? CarBody {get; set;}

        /// <summary>
        /// Categoria do Veículo
        /// </summary>
        public virtual VehicleCategory VehicleCategory {get; set;}

        public virtual string Image {get; set;}
    }
}
