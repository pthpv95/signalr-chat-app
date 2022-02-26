using System;

namespace chat_service.Contracts
{
    public class AcceptFriendRequest
    {
        public Guid UserId { get; set; }

        public Guid RequestId { get; set; }
    }
}