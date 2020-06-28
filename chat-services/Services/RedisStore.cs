using System;
using StackExchange.Redis;

namespace chatservices.Services
{
    public class RedisStore
    {
        private readonly Lazy<ConnectionMultiplexer> _lazyConnection;

        public ConnectionMultiplexer Connection => _lazyConnection.Value;

        public RedisStore()
        {
            var configurationOptions = new ConfigurationOptions
            {
                AbortOnConnectFail = false,
                //Password = "p2dd84b4346a86025a9b8422ba39664ffa81fdb575d377a6424aa3980be9ad394",
                //EndPoints =
                //{
                //{ "ec2-54-160-105-150.compute-1.amazonaws.com", 28389 }
                //}
                EndPoints = { "13.236.84.42:6379" }
            };

            _lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(configurationOptions));

        }

        public IDatabase RedisCache => Connection.GetDatabase();
    }
}
