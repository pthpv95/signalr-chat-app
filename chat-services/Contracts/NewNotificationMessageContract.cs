using System;
using System.Collections;
using System.Collections.Generic;

namespace chatservices.Contracts
{
    public class NewNotificationMessageContract
    {
        public int TotalNoti { get; set; }

        public int TotalUnreadMessages { get; set; }

        public List<string> ConnectionIds { get; set; }

        public NotificationActionType ActionType { get; set; }
    }

    public enum NotificationActionType
    {
        NewNoti,
        UnreadMessage
    }
}
