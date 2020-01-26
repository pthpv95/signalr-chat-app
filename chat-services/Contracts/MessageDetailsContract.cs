using System;

namespace realtime_app.Contracts
{
    public class MessageDetailsContract
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public string AttachmentUrl { get; set; }

        public int MessageType { get; set; }

        public string SentAt { get; set; }

        public bool IsResponse { get; set; }
    }
}