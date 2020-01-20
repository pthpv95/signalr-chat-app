using System;

namespace realtime_app.Contracts
{
    public class UserConversationInfoRequest
    {
        public Guid ContactUserId { get; set; }

        public Guid ConversationId { get; set; }
    }
}