using System;
using chat_service.Common;

namespace chat_service.Models
{
    public class Notification : AggregateRootBase
    {
        protected Notification()
        {
        }

        public Guid RecipientId { get; set; }

        public Guid SenderId { get; set; }

        public bool Read { get; set; }

        public string Content { get; set; }

        public NotificationType Type { get; set; }

        public virtual User Recipient { get; set; }

        public virtual User Sender { get; set; }
    }
}
