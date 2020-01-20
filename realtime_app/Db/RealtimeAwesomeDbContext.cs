using Microsoft.EntityFrameworkCore;
using realtime_app.Models.ModelBuilders;

namespace realtime_app.Db
{
    public class RealtimeAwesomeDbContext : DbContext
    {
        public RealtimeAwesomeDbContext(DbContextOptions<RealtimeAwesomeDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            MessageBuilder.Build(builder);
            UserBuilder.Build(builder);
            ConversationBuilder.Build(builder);
            FriendsRequestBuilder.Build(builder);
            MessageBuilder.Build(builder);
            UserContactBuilder.Build(builder);
            ContactBuilder.Build(builder);
            NotificationBuilder.Build(builder);
            NotificationTypeBuilder.Build(builder);
            ParticipantBuilder.Build(builder);
        }
    }
}