using System;

namespace realtime_app.Contracts
{
    public class UserContactContract
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid? ConversationId { get; set; }
    }
}