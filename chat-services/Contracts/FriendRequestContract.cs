using System;

namespace chat_service.Contracts
{
    public class FriendRequestContract
    {
        public Guid Id { get; set; }

        public Guid ContactId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}