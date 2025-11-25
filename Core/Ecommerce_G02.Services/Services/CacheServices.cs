using Ecommerce_G02.Abstractions.IServices;
using Ecommerce_G02.Domain.Contacts.IRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Ecommerce_G02.Services.Services
{
    public class CacheServices(ICacheReposatory CacheRepo) : ICacheServices
    {
        public async Task<string?> GetCacheAsync(string cacheKey)
        {
          return await CacheRepo.GetCacheAsyn(cacheKey);
        }

        public async Task SetCacheAsync(string cacheKey, object CacheValue, TimeSpan TimeToLive)
        {
           var value=JsonSerializer.Serialize(CacheValue);
            await CacheRepo.SetCacheAsyn(cacheKey, value, TimeToLive);
        }
    }
}
