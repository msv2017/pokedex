using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pokedex.Application
{

    public interface ICacheService
    {
        Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> valueFactory);
    }

    /// <summary>
    /// Simplest possible in-memory caching.
    /// Should be used to cache static resources only.
    /// </summary>
    public class CacheService : ICacheService
    {
        private readonly Dictionary<string, string> _cache;

        public CacheService()
        {
            _cache = new Dictionary<string, string>();
        }

        public async Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> valueFactory)
        {
            if (!_cache.TryGetValue(key, out var value))
            {
                var item = await valueFactory();
                _cache[key] = JsonSerializer.Serialize(item);
                return item;
            }

            return JsonSerializer.Deserialize<T>(value);
        }
    }
}
