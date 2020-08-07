using System;
namespace chatservices.Contracts
{
    public class MessageHasSeenReponseContract
    {
        public Guid ConversationId { get; set; }

        public Guid MessageId { get; set; }

        public Guid SeenerId { get; set; }
    }
}
