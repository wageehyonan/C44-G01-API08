using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Domain.Contacts.IRepos
{
    public interface ICacheReposatory
    {
        public Task<string?> GetCacheAsyn(string CacheKey);

        public Task SetCacheAsyn(string CacheKey, string CacheValue,TimeSpan TimeToLive);
    }
}
