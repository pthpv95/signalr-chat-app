using System;
using System.Collections.Generic;
using realtime_app.Common;

namespace realtime_app.Models
{
    public class UserMessage : ValueObjectBase
    {
        protected UserMessage()
        {

        }

        public Guid UserId { get; set; }

        public Guid MessageId { get; set; }

        public User User { get; set; }

        public Message Message { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return UserId;
            yield return MessageId;
        }
    }
}