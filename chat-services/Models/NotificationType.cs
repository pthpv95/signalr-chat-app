using chat_service.Common;

namespace chat_service.Models
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
