using Ecommerce_G02.Domain.Contacts.IRepos;
using Ecommerce_G02.Domain.Contacts.IUOW;
using Ecommerce_G02.Domain.Models;
using Ecommerce_G02.Presistence.Contexts;
using Ecommerce_G02.Presistence.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Presistence.UOW
{
    public class UnitOfWork(StoreDBContext _context) : IUnitOfWork
    {
        private readonly Dictionary<string,object> Dic_Repos = [];
        public    IGenericReposatory<Tentity, Tkey> GetReposatory<Tentity, Tkey>() where Tentity : BaseEntity<Tkey>
        {
            var EntityName = typeof(Tentity).Name;
            if (Dic_Repos.ContainsKey(EntityName))
                {
                return (IGenericReposatory<Tentity,Tkey>) Dic_Repos[EntityName];
            }
            else
            {
               var repo = new GenericReposatory<Tentity,Tkey> (_context);
                Dic_Repos.Add(EntityName, repo);
                return repo;
            }

            
        }

        public async Task<int> SaveChangesAsync()
        {
           return await _context.SaveChangesAsync();
        }
    }
}
