using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Cache.Contracts;
using Infrastructure.Extensions;
using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.Cache
{
    public class Cache : ICache
    {
        private readonly IDistributedCache _distributedCache;

        public Cache(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T> Get<T>(string cacheKey, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentException($"{nameof(cacheKey)} cannot be null or empty");
            var response = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);

            return response.IsJsonStringEmpty() ? default : JsonSerializer.Deserialize<T>(response);
        }

        public async Task Set<T>(string cacheKey, T response, CancellationToken cancellationToken, int timeToLive = 10) where T : new()
        {
            if (string.IsNullOrWhiteSpace(cacheKey))
                throw new ArgumentException($"{nameof(cacheKey)} cannot be null or empty");
            if (response == null)
                throw new ArgumentException($"{nameof(response)} cannot be null");
            await _distributedCache.SetStringAsync(cacheKey, JsonSerializer.Serialize(response), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = new TimeSpan(0, timeToLive, 0)
            }, cancellationToken);
        }
    }
}