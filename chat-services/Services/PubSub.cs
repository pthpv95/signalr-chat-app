using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace chatservices.Services
{
    public class PubSub : IPubSub
    {
        private readonly RedisStore _redisStore;
        public PubSub(RedisStore redisStore)
        {
            _redisStore = redisStore;
        }

        public async Task Publish<T>(string channelName, T message)
        {
            var sub = _redisStore.RedisCache.Multiplexer.GetSubscriber();
            await sub.PublishAsync(channelName, JsonConvert.SerializeObject(message));
        }

        public async Task Subscribe<T>(string channelName, Action<T> eventHandler)
        {
            var sub = _redisStore.RedisCache.Multiplexer.GetSubscriber();
            await sub.SubscribeAsync(channelName, (channel, message) =>
            {
                var payload = JsonConvert.DeserializeObject<T>(message);
                eventHandler(payload);
            });
        }
    }
}
