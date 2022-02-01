using CarSystem.Domain.Base;

namespace CarSystem.Domain
{
    public class VehicleCategory : Entity
    {
        /// <summary>
        /// Nome da Categoria
        /// </summary>
        public virtual string Name {get; set;}
    }
}