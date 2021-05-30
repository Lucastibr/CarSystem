using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarSystem.Domain.Repository;
using Codout.Framework.DAL;

namespace CarSystem.Domain
{
    public interface IUnitOfWorkCarSystem : IUnitOfWork
    {
        IVechileRepository Vehicle {get; }
        IEnterpriseRepository Enterprise {get;}
    }
}
