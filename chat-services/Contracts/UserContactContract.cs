using System;

namespace chat_service.Contracts
{
    public class UserContactContract
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}