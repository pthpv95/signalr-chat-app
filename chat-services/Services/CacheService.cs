using System;
using System.IO;
using System.Threading.Tasks;
using MessagePack;
using Microsoft.Extensions.Caching.Memory;

namespace chatservices.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T Get<T>(string key)
        {
            try
            {
                T item = (T)_memoryCache.Get(key);
                return item;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void Set<T>(string key, T value)
        {
            try
            {
                if (!string.IsNullOrEmpty(key))
                {
                    _memoryCache.Set(key, value);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
