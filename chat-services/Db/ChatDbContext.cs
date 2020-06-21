using chat_services.Models.ModelBuilders;
using Microsoft.EntityFrameworkCore;
using realtime_app.Models.ModelBuilders;

namespace realtime_app.Db
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
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
            FileStorageBuilder.Build(builder);
        }
    }
}