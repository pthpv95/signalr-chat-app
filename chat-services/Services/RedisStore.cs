using System;
using chatservices.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace chatservices.Services
{
    public class RedisStore
    {
        private readonly RedisSettings _redisSettings;
        private readonly Lazy<ConnectionMultiplexer> _lazyConnection;

        public ConnectionMultiplexer Connection => _lazyConnection.Value;

        public RedisStore(IOptions<RedisSettings> redisSettings)
        {
            _redisSettings = redisSettings.Value;
            var configurationOptions = new ConfigurationOptions
            {
                AbortOnConnectFail = false,
                Password = _redisSettings.Password,
                EndPoints = { { _redisSettings.Host, _redisSettings.Port } }
            };

            _lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(configurationOptions));
        }

        public IDatabase RedisCache => Connection.GetDatabase();
    }
}
