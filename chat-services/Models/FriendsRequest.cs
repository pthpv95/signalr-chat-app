using System;
using chat_service.Common;

namespace chat_service.Models
{
    public class FriendsRequest : AggregateRootBase
    {

        protected FriendsRequest(){}

        public FriendsRequest(Guid receiverId, Guid requesterId)
        {
            this.ReceiverId = receiverId;
            this.RequesterId = requesterId;
            this.Created = DateTime.Now;
            this.Status = FriendsRequestEnum.PENDING;
        }

        public Guid ReceiverId { get; set; }

        public Guid RequesterId { get; set; }

        public FriendsRequestEnum Status { get; set; }
    }

    public enum FriendsRequestEnum
    {
        PENDING,
        ACCEPTED
    }
}