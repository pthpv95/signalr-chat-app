using System;

namespace chat_service.Contracts
{
    public class UserConversationInfoRequest
    {
        public Guid ContactUserId { get; set; }

        public Guid ConversationId { get; set; }
    }
}