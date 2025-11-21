using DomainLayer.Contracts;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services
{
    internal class CacheService(ICacheRepository _cache) : ICacheService
    {
        public Task<string?> GetAsync(string key)
        {
            var value = _cache.GetAsync(key);
            return value;
        }

        public async Task SetAsync(string key, object value, TimeSpan duration)
        {
            var val = JsonSerializer.Serialize(value);
            await _cache.SetAsync(key, val, duration);
            
            
        }
    }
}
