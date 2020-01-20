using System;
using System.Threading.Tasks;

namespace realtime_app.Services
{
    public interface INotify
    {
        Task DispatchNewNotification();
    }
}
