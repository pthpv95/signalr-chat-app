using System.Collections.Generic;
using realtime_app.Common;

namespace realtime_app.Models
{
    public class Message : AggregateRootBase
    {
        public string Text { get; set; }

        public ICollection<UserMessage> UserMessage { get; set; }
    }
}