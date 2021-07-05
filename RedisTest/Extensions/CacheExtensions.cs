using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace RedisTest.Extensions
{
    public static class CacheExtensions
    {
        public static async Task SetRecordAsync<T>(this IDistributedCache cache, string recordId, T data, TimeSpan? absoluteExpireTime = null, TimeSpan? slidingExpireTime = null)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpireTime ?? TimeSpan.FromSeconds(30),
                SlidingExpiration = slidingExpireTime
            };
            string dataStr = JsonConvert.SerializeObject(data);
            await cache.SetStringAsync(recordId, dataStr, options);
        }

        public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string recordId)
        {
            string dataStr = await cache.GetStringAsync(recordId);
            if (dataStr is null)
                return default;
            else
                return JsonConvert.DeserializeObject<T>(dataStr);
        }
    }
}