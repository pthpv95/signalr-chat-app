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
        }

        public int RecieverId { get; set; }

        public int RequesterId { get; set; }

        public FriendsRequestStatus Status { get; set; }

        public virtual User Requester { get; set; }

        public virtual User Reciever { get; set; }
    }

    public enum FriendsRequestStatus 
    {
        PENDING,
        ACCEPPTED
    }
}