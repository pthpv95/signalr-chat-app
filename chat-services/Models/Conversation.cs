using System;
using System.Collections.Generic;
using realtime_app.Common;

namespace realtime_app.Models
{
    public class Conversation : AggregateRootBase
    {
        protected Conversation() {}

        public Conversation(string title, Guid creatorId)
        {
            this.Title = title;
            this.CreatorId = creatorId;
        }

        public string Title { get; set; }

        public Guid CreatorId { get; set; }  
        
        public User Creator { get; set; }
    }
}