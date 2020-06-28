using System;
using System.IO;
using System.Threading.Tasks;
using MessagePack;

namespace chatservices.Services
{
    public class CacheService : ICacheService
    {
        private readonly RedisStore _redisStore;

        public CacheService(RedisStore redisStore)
        {
            _redisStore = redisStore;
        }

        public async Task<T> Get<T>(string key)
        {
            var cachedValue = await _redisStore.Database.StringGetAsync(key);
            if (cachedValue.IsNullOrEmpty) return default;

            byte[] data = Convert.FromBase64String(cachedValue);
            using (var ms = new MemoryStream(data))
            {
                var deserializedObject = MessagePackSerializer.Typeless.Deserialize(ms);
                return (T)deserializedObject;
            }
        }

        public async Task Set(string key, object value)
        {
            var bytes = MessagePackSerializer.Typeless.Serialize(value);
            var data = Convert.ToBase64String(bytes);
            await _redisStore.Database.StringSetAsync(key, data);
        }
    }
}
