using Ecommerce_G02.Domain.Contacts.ISpecifications;
using Ecommerce_G02.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Domain.Contacts.IRepos
{
    public interface IGenericReposatory<Tentity,Tkey> where Tentity :BaseEntity<Tkey>
    {
        Task<IEnumerable<Tentity>> GetAllAsync();
        Task<Tentity> GetByIdAsync(Tkey id);

        void Add(Tentity entity);
        void Update(Tentity entity);
        void Delete(Tentity entity);

        Task<IEnumerable<Tentity>> GetAllWithSpecificationAsync(ISpecification<Tentity, Tkey> _specification);
        Task<Tentity> GetByIdWithSpecifiactionAsync(ISpecification<Tentity,Tkey> _specification);

        Task<int> GetCountWithSpecificationAsync(ISpecification<Tentity, Tkey> _specification);
    }
}
