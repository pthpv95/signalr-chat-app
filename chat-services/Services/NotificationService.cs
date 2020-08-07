using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using realtime_app.Db;
using realtime_app.Models;

namespace realtime_app.Services
{
    public class NotificationService : INotificationService
    {
        private ChatDbContext _context;

        public NotificationService(ChatDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetUserNumberOfNotifications(Guid userId)
        {
            var pendingFriendRequests = await _context.Set<FriendsRequest>()
                .AnyAsync(x => x.ReceiverId == userId && x.Status == FriendsRequestEnum.PENDING) ? 1 : 0;


            return pendingFriendRequests;
        }
    }
}
