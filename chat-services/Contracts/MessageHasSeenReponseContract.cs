using System;
namespace chatservices.Contracts
{
    public class MessageHasSeenResponseContract
    {
        public Guid ConversationId { get; set; }

        public Guid MessageId { get; set; }

        public Guid SeenerId { get; set; }
    }
}
