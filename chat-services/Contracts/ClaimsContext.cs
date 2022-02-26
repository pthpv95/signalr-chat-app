using System;

namespace chat_service.Contracts
{
    public class ClaimsContext
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }
    }
}