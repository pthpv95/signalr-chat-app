using System;

namespace realtime_app.Contracts
{
    public class UserContract
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }
    }
}