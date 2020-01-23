using System;
using System.Linq;
using System.Threading.Tasks;
using realtime_app.Db;
using realtime_app.Models;

namespace realtime_app.Services
{
    public class NotificationService : INotificationService
    {
        private RealtimeAwesomeDbContext _context;

        public NotificationService(RealtimeAwesomeDbContext context)
        {
            _context = context;
        }

        public Task<int> GetUserNumberOfNotifications(Guid userId)
        {
            var pendingFriendRequests = _context.Set<FriendsRequest>()
                .Where(x => x.ReceiverId == userId && x.Status == FriendsRequestEnum.PENDING)
                .Count();



            throw new NotImplementedException();
        }
    }
}
