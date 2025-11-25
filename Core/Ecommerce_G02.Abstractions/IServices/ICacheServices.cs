using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_G02.Abstractions.IServices
{
    public interface ICacheServices
    {
        public Task<string?> GetCacheAsync(string cacheKey);
        public Task SetCacheAsync(string cacheKey, object CacheValue,TimeSpan TimeToLive);
    }
}
