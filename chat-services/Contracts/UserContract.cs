using System;

namespace chat_service.Contracts
{
    public class UserContract
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AbbreviatedName { get; set; }

        public string UserName { get; set; }
    }
}