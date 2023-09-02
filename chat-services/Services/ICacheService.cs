using System;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace chatservices.Services
{
    public interface ICacheService
    {
        /// <summary>
        /// Get Data using key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// Set Data with Value and Expiration Time of Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expirationTime"></param>
        /// <returns></returns>
        void Set<T>(string key, T value);
    }
}
