using System;
using System.Collections.Generic;
using chat_service.Common;
using chat_service.Models;

namespace chatservices.Models
{
    public class ReadReceipt : ValueObjectBase
    {
        protected ReadReceipt()
        {
        }

        public ReadReceipt(Guid messageId, Guid receiverId, Guid conversationId)
        {
            MessageId = messageId;
            SeenerId = receiverId;
            ConversationId = conversationId;
        }

        public Guid MessageId { get; set; }

        public Message Message { get; set; }

        public Guid SeenerId { get; set; }

        public Guid ConversationId { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
    }
}
