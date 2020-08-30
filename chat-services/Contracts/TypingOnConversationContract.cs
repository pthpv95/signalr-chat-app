using System;

namespace chat_services.Contracts
{
    public class TypingOnConversationContract
    {
        public Guid ContactUserId { get; set; }

        public Guid ConversationId { get; set; }
    }
}