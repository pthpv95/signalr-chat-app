using System.Collections.Generic;
using realtime_app.Common;

namespace realtime_app.Models
{
    public class User : AggregateRootBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual Conversation Conversation { get; set; }

        public User(string firstName, string lastName)
        {
            this.FirstName = firstName;
            
            this.LastName = lastName;
        }
    }
}