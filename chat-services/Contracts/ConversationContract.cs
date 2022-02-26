using System;
using System.Collections.Generic;

namespace chat_service.Contracts
{
    public class ConversationContract
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public IList<MessageDetailsContract> Messages { get; set; }
    }
}