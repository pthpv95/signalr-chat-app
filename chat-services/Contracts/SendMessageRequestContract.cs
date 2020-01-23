using System;

namespace realtime_app.Contracts
{
    public class SendMessageRequestContract
    {
        public Guid ContactId { get; set; }

        public Guid SenderId { get; set; }
        
        public string Message { get; set; }
    }
}