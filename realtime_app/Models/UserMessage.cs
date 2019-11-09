using System.Collections.Generic;
using realtime_app.Common;

namespace realtime_app.Models
{
    public class UserMessage : ValueObjectBase
    {
        protected UserMessage()
        {

        }

        public int UserId { get; set; }

        public int MessageId { get; set; }

        public User User { get; set; }

        public Message Message { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return UserId;
            yield return MessageId;
        }
    }
}