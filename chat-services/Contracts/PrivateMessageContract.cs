using System;
using System.Collections.Generic;
using chat_services.Contracts;
using chat_service.Contracts;

namespace chatservices.Contracts
{
    public class PrivateMessageContract
    {
        public List<string> ConnectionIds { get; set; }

        public PrivateMessageActionType ActionType { get; set; }

        public SendMessageRequestContract NewMessage { get; set; }

        public MessageHasSeenResponseContract SeenMessage { get; set; }

        public TypingOnConversationContract TypingOnConversation { get; set; }
    }

    public enum PrivateMessageActionType
    {
        NewMessage,
        SeenMessage,
        Typing,
        StopTyping
    }
}
