using realtime_app.Common;

namespace realtime_app.Models
{
    public class Contact : AggregateRootBase
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

    }
}