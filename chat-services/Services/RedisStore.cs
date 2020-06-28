using System;
using StackExchange.Redis;

namespace chatservices.Services
{
    public class RedisStore
    {
        public RedisStore()
        {

        }

        public IDatabase Database
        {
            get
            {
                var redis = ConnectionMultiplexer.Connect(new ConfigurationOptions
                {
                    AllowAdmin = false,
                    Ssl = false,
                    Password = "p2dd84b4346a86025a9b8422ba39664ffa81fdb575d377a6424aa3980be9ad394",
                    EndPoints =
                    {
                        { "ec2-54-160-105-150.compute-1.amazonaws.com", 28389 }
                    }
                });

                return redis.GetDatabase();
            }
        }
    }
}
