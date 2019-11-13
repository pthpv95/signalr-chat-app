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

        public Message(string text, int senderId, int messageType, int conversationId)
        {
            this.Text = text;
            this.SenderId = senderId;
            this.MessageType = (MessageType)messageType;
            this.ConversationId = conversationId;
            this.Created = DateTime.Now;
        }

        public string Text { get; set; }

        public int SenderId { get; set; }

        public int ConversationId { get; set; }

        public string AttachmentUrl { get; set; }

        public MessageType MessageType { get; set; }     

        public virtual User Sender { get; set; }

        public virtual Conversation Conversation { get; set; }
    }

    public enum MessageType 
    {
        Text,
        AttachmentUrl,
    }
}