using Ecommerce_G02.Domain.Contacts.IRepos;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Presistence.Repos
{
    public class CacheReposatory (IConnectionMultiplexer _connection): ICacheReposatory
    {
        private  IDatabaseAsync _database=_connection.GetDatabase();
        public async Task<string?> GetCacheAsyn(string CacheKey)
        {
            var cachevalue = await _database.StringGetAsync(CacheKey);
            return cachevalue.IsNullOrEmpty ? null : cachevalue.ToString();
        }

        public async Task SetCacheAsyn(string CacheKey, string CacheValue, TimeSpan TimeToLive)
        {
            await _database.StringSetAsync(CacheKey, CacheValue, TimeToLive); 
        }
    }
}
