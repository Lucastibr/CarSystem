using System.Collections.Generic;
using System.Collections.ObjectModel;
using CarSystem.Domain.Base;

namespace CarSystem.Domain
{
    public class Enterprise : Entity
    {
        private readonly ISet<Vehicle> _vehicles = new HashSet<Vehicle>();

        /// <summary>
        /// Nome da Empresa
        /// </summary>
        public virtual string CompanyName {get; set;}

        /// <summary>
        /// CNPJ da Empresa
        /// </summary>
        public virtual string Cnpj {get; set;}
        
        /// <summary>
        /// Lista de Veículos
        /// </summary>
        public virtual IReadOnlyCollection<Vehicle> Vehicles => new ReadOnlyCollection<Vehicle>(new List<Vehicle>(_vehicles));
    }
}
