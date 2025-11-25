using Ecommerce_G02.Domain.Contacts.IRepos;
using Ecommerce_G02.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Domain.Contacts.IUOW
{
    public interface IUnitOfWork
    {

        public IGenericReposatory<Tentity, Tkey> GetReposatory<Tentity, Tkey>() where Tentity : BaseEntity<Tkey>;
        Task<int> SaveChangesAsync();
    }
}
