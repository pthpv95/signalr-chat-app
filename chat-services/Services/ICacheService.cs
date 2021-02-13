using System;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace chatservices.Services
{
    public interface ICacheService
    {
        Task Set(string key, object value);
        Task<T> Get<T>(string key);
    }
}
