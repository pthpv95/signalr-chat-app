using System;
using System.Collections.Generic;
using realtime_app.Common;
using realtime_app.Models;

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
