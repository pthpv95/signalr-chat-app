using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using chatservices.Models;
using Microsoft.EntityFrameworkCore.Internal;
using chat_service.Common;

namespace chat_service.Models
{
    public class Message : AggregateRootBase
    {
        protected Message()
        {

        }

        public Message(string text, Guid senderId, MessageType messageType, Guid conversationId, string attachmentUrl)
        {
            Text = text;
            AttachmentUrl = attachmentUrl;
            SenderId = senderId;
            MessageType = messageType;
            ConversationId = conversationId;
            Created = DateTime.Now;
        }

        public string Text { get; set; }

        public Guid SenderId { get; set; }

        public Guid ConversationId { get; set; }

        public Conversation Conversation { get; set; }

        public string AttachmentUrl { get; set; }

        public MessageType MessageType { get; set; }

        public virtual User Sender { get; set; }

        public ICollection<ReadReceipt> ReadReceipts { get; set; }

        public void Read(Guid receiverId)
        {
            ReadReceipts.Clear();
            ReadReceipts = new List<ReadReceipt> { new ReadReceipt(Id, receiverId, ConversationId) };
        }
    }

    public enum MessageType
    {
        Text,
        AttachmentUrl,
    }
}