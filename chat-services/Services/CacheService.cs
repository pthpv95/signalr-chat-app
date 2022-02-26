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
            try
            {
                var cachedValue = await _redisStore.RedisCache.StringGetAsync(key);
                if (cachedValue.IsNullOrEmpty) return default;

                byte[] data = Convert.FromBase64String(cachedValue);
                using var ms = new MemoryStream(data);
                var deserializedObject = MessagePackSerializer.Typeless.Deserialize(ms);
                return (T)deserializedObject;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Set(string key, object value)
        {
            var bytes = MessagePackSerializer.Typeless.Serialize(value);
            var data = Convert.ToBase64String(bytes);
            await _redisStore.RedisCache.StringSetAsync(key, data);
        }
    }
}
