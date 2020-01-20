using System;

namespace realtime_app.Contracts
{
    public class MessageDetailsContract
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public string SentAt { get; set; }

        public bool IsResponse { get; set; }
    }
}