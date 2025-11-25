using Ecommerce_G02.Domain.Models.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Domain.Contacts.IRepos
{
    public interface IBasketReposatory
    {
        Task<CustomerBasket?> GetBasketAsync(string key);
        Task <CustomerBasket?> CreateUpdateAsync(CustomerBasket basket,TimeSpan? TimeToLive=null);
        Task <bool> DeleteBasketAsync(string key);
    }
}
