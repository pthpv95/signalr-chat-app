using System;
using System.Collections.Generic;
using chat_service.Common;

namespace chat_service.Models
{
    public class Contact : AggregateRootBase
    {
        public Contact(Guid userId, string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.UserId = userId;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid UserId { get; set; }

        public IList<UserContact> UserContacts { get; set; }
    }
}