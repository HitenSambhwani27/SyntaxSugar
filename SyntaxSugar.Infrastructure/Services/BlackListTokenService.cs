using StackExchange.Redis;
using SyntaxSugar.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxSugar.Infrastructure.Services
{
    public class BlackListTokenService : IBlacklistTokenService
    {
        private readonly IDatabase _redisDb;

        private static string GetKey(string token)
        {
            return $"blacklist:{token}";
        }
        public BlackListTokenService(IConnectionMultiplexer redisConnection)
        {
            this._redisDb = redisConnection.GetDatabase();
        }
        public async Task BlackListTokens(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var expiry = jwtToken.ValidTo;

            var timeToLive = expiry.ToUniversalTime() - DateTime.UtcNow;
            if (timeToLive > TimeSpan.Zero)
            {
                await _redisDb.StringSetAsync(GetKey(token), "", timeToLive);
            }

        }

        public async Task<bool> IsTokenBlacklisted(string token)
        {
            return await _redisDb.KeyExistsAsync(GetKey(token));
        }
    }
}
