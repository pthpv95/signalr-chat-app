using realtime_app.Common;

namespace realtime_app.Models
{
    public class UserContact : AggregateRootBase
    {
        public int UserId { get; set; }

        public int ContactId { get; set; }

        public virtual User User { get; set; }

        public virtual Contact Contact { get; set; }
    }
}