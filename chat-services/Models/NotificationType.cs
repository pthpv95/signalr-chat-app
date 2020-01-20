using realtime_app.Common;

namespace realtime_app.Models
{
    public class NotificationType : AggregateRootBase
    {
        protected NotificationType()
        {
        }

        public string Name { get; set; }

        public NotificationTypeEnum Type { get; set; }

        public string Description { get; set; }
    }

    public enum NotificationTypeEnum
    {
        FriendRequested,
        FriendRequestAccepted
    }
}
