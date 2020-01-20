using System;

namespace realtime_app.Contracts
{
    public class AcceptFriendRequest
    {
        public Guid UserId { get; set; }

        public Guid RequestId { get; set; }
    }
}