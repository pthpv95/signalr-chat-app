using System;
using realtime_app.Common;

namespace realtime_app.Models
{
    public class UserContact : AggregateRootBase
    {

        public UserContact(Guid userId, Guid contactId)
        {
            UserId = userId;
            ContactId = contactId;
        }
        
        public Guid UserId { get; set; }

        public Guid ContactId { get; set; }

        public virtual User User { get; set; }

        public virtual Contact Contact { get; set; }
    }
}