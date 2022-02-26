using System;
using System.Collections.Generic;
using chat_service.Common;
using chat_service.Models;

namespace chatservices.Models
{
    public class Member : ValueObjectBase
    {
        protected Member()
        {
        }

        public Member(Guid conversationId, Guid userId)
        {
            UserId = userId;
            ConversationId = conversationId;
        }

        public Guid UserId { get; set; }

        public Guid ConversationId { get; set; }

        public Conversation Conversation { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
    }
}
