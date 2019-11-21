using System;
using realtime_app.Common;

namespace realtime_app.Models
{
    public class FriendsRequest : AggregateRootBase
    {

        protected FriendsRequest(){}

        public FriendsRequest(int receiverId, int requesterId)
        {
            this.RecieverId = receiverId;
            this.RequesterId = requesterId;
            this.Created = DateTime.Now;
            this.Status = FriendsRequestEnum.PENDING;
        }

        public int RecieverId { get; set; }

        public int RequesterId { get; set; }

        public FriendsRequestEnum Status { get; set; }
    }

    public enum FriendsRequestEnum
    {
        PENDING,
        ACCEPTED
    }
}