using System;
using System.Collections.Generic;
using realtime_app.Common;

namespace realtime_app.Models
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
