using System.Collections.Generic;
using realtime_app.Common;

namespace realtime_app.Models
{
    public class Conversation : AggregateRootBase
    {
        protected Conversation() {}

        public Conversation(string title, int creatorId)
        {
            this.Title = title;
            this.CreatorId = creatorId;
        }

        public string Title { get; set; }

        public int CreatorId { get; set; }  
        
        public ICollection<User> Users { get; set; }

        public ICollection<Message> Messages { get; set; }

    }
}