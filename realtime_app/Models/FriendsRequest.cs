using realtime_app.Common;

namespace realtime_app.Models
{
    public class FriendsRequest : AggregateRootBase
    {
        public int RecieverId { get; set; }

        public int RequesterId { get; set; }

        public virtual User Requester { get; set; }

        public virtual User Reciever { get; set; }
    }
}