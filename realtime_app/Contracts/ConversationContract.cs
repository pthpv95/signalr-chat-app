using System;
using System.Collections.Generic;

namespace realtime_app.Contracts
{
    public class ConversationContract
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public IList<MessageDetailsContract> Messages { get; set; }
    }
}