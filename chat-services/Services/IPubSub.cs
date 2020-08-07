using System;
using System.Threading.Tasks;

namespace chatservices.Services
{
    public interface IPubSub
    {
        Task Publish<T>(string channelName, T message);
        
        Task Subscribe<T>(string channelName, Action<T> handler);
    }
}
