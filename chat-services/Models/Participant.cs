using System;
using System.Collections.Generic;
using chat_service.Common;

namespace chat_service.Models
{
  public class Participant : AggregateRootBase
  {
    protected Participant()
    {
    }

    public Participant(Guid conversationId, ParticipanTypeEnum type, Guid userId)
    {
      ConversationId = conversationId;
      Type = type;
      UserId = userId;
    }

    public Guid ConversationId { get; set; }

    public Guid UserId { get; set; }

    public ParticipanTypeEnum Type { get; set; }
  }

  public enum ParticipanTypeEnum
  {
    Private,
    InGroup
  }
}
