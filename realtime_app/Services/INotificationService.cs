using System;
using System.Threading.Tasks;

namespace realtime_app.Services
{
    public interface INotificationService
    {
        Task<int> GetUserNumberOfNotifications(Guid userId);
    }
}
