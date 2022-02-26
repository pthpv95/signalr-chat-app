using System;

namespace chat_service.Contracts
{
    public class RequestAddFriendContract
    {
        public Guid RequesterId { get; set; }

        public Guid ReceiverId { get; set; }
    }
}