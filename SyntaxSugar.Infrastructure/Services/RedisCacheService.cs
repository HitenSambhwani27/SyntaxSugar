using StackExchange.Redis;
using SyntaxSugar.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SyntaxSugar.Infrastructure.Services
{
    public class RedisCacheService : ICacheService
    {
        private IDatabase database;

        public RedisCacheService(IConnectionMultiplexer connection)
        {
                this.database = connection.GetDatabase();
        }
        public async Task DeleteAsync(string key)
        {
             await database.KeyDeleteAsync(key);
        }

        public async Task<T> GetAsync<T>(string key)
        {
           var value = await database.StringGetAsync(key);
            if (value.IsNullOrEmpty)
            {
                return default(T);
            }
            return JsonSerializer.Deserialize<T>(value);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            await database.StringSetAsync(key, JsonSerializer.Serialize(value), expiration);
        }
    }
}
