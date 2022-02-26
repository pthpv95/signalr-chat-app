using System;
using System.Threading.Tasks;

namespace chat_service.Services
{
    public interface INotificationService
    {
        Task<int> GetUserNumberOfNotifications(Guid userId);
    }
}
