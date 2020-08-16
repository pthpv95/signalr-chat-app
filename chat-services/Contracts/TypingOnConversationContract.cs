using System;

namespace chat_services.Contracts
{
    public class TypingOnConversationContract
    {
        public Guid UserId { get; set; }

        public Guid Conversationid { get; set; }
    }
}