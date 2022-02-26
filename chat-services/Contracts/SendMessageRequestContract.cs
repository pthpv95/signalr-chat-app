using System;

namespace chat_service.Contracts
{
  public class SendMessageRequestContract
  {
    public Guid MessageId { get; set; }
    public Guid ContactUserId { get; set; }

    public Guid SenderId { get; set; }

    public string Message { get; set; }

    public int MessageType { get; set; }

    public string AttachmentUrl { get; set; }

    public Guid ConversationId { get; set; }


  }
}