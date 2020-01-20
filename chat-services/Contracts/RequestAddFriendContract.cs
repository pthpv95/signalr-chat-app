using System;

namespace realtime_app.Contracts
{
    public class RequestAddFriendContract
    {
        public Guid RequesterId { get; set; }

        public Guid ReceiverId { get; set; }
    }
}