using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Pokedex.Application
{

    public interface ICacheService
    {
        Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> valueFactory);
    }

    public class RedisCacheService : ICacheService
    {
        private readonly ILogger _logger;
        private readonly IDistributedCache _cache;

        public RedisCacheService(ILogger<RedisCacheService> logger, IDistributedCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        public async Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> valueFactory)
        {
            try
            {
                var value = await _cache.GetStringAsync(key);

                if (string.IsNullOrWhiteSpace(value))
                {
                    var item = await valueFactory();
                    value = JsonSerializer.Serialize(item);
                    await _cache.SetStringAsync(key, value, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
                    });
                }

                return JsonSerializer.Deserialize<T>(value);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return await valueFactory();
            }
        }
    }
}
