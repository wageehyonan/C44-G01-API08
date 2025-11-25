using Ecommerce_G02.Domain.Contacts.IRepos;
using Ecommerce_G02.Domain.Contacts.ISpecifications;
using Ecommerce_G02.Domain.Models;
using Ecommerce_G02.Presistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Presistence.Repos
{
    public class GenericReposatory<Tentity, Tkey> (StoreDBContext _context): IGenericReposatory<Tentity, Tkey> where Tentity : BaseEntity<Tkey>
    {

        public async Task<IEnumerable<Tentity>> GetAllAsync() 
            
            => await _context.Set<Tentity>().ToListAsync();

        public async Task<Tentity> GetByIdAsync(Tkey id)
                
            =>await _context.Set<Tentity>().FindAsync(id);

        public void Add(Tentity entity)
         
             => _context.Set<Tentity>().Add(entity);
        
        public void Update(Tentity entity)
              => _context.Set<Tentity>().Update(entity);

        public void Delete(Tentity entity)
              => _context.Set<Tentity>().Remove(entity);

        public async Task<IEnumerable<Tentity>> GetAllWithSpecificationAsync(ISpecification<Tentity, Tkey> _specification)

        {
            var Query=await SpecificationEvlautor.CreateQuery(_context.Set<Tentity>(),_specification).ToListAsync();
            return Query;
        }
        public async Task<Tentity> GetByIdWithSpecifiactionAsync(ISpecification<Tentity, Tkey> _specification)
        {
            var Query = await SpecificationEvlautor.CreateQuery(_context.Set<Tentity>(), _specification).FirstOrDefaultAsync();
            return Query;
        }

        public async Task<int> GetCountWithSpecificationAsync(ISpecification<Tentity, Tkey> _specification)
        {
            var Query = await SpecificationEvlautor.CreateQuery(_context.Set<Tentity>(), _specification).ToListAsync();
            return   Query.Count;
        }
    }
}
