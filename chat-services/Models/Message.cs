using System;
using System.Collections.Generic;
using realtime_app.Common;

namespace realtime_app.Models
{
    public class Message : AggregateRootBase
    {
        protected Message()
        {

        }

        public Message(string text, Guid senderId, MessageType messageType, Guid conversationId, string attachmentUrl)
        {
            this.Text = text;
            this.AttachmentUrl = attachmentUrl;
            this.SenderId = senderId;
            this.MessageType = messageType;
            this.ConversationId = conversationId;
            this.Created = DateTime.Now;
        }

        public string Text { get; set; }

        public Guid SenderId { get; set; }

        public Guid ConversationId { get; set; }

        public string AttachmentUrl { get; set; }

        public MessageType MessageType { get; set; }     

        public virtual User Sender { get; set; }
    }

    public enum MessageType 
    {
        Text,
        AttachmentUrl,
    }
}