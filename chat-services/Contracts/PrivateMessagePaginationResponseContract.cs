using System;
using System.Collections.Generic;
using chat_service.Contracts;

namespace chat_services.Contracts
{
    public class PrivateMessagePaginationResponseContract
    {
        public DateTime? NextCursor { get; set; }
        public ConversationContract Conversation { get; set; }
    }
}