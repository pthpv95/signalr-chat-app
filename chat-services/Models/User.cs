using System;
using System.Collections.Generic;
using chat_service.Common;

namespace chat_service.Models
{
    public class User : AggregateRootBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public IList<UserContact> UserContacts { get; set; }

        public User(string firstName, string lastName, string userName)
        {
            this.FirstName = firstName;
            
            this.LastName = lastName;

            this.UserName = userName;
        }
    }
}