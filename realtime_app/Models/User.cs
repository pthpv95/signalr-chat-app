using System.Collections.Generic;
using realtime_app.Common;

namespace realtime_app.Models
{
    public class User : AggregateRootBase
    {
        public string Name { get; set; }

        public ICollection<UserMessage> UserMessages { get; set; }     
    }
}