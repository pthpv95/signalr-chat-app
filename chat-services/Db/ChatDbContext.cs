using chat_services.Models.ModelBuilders;
using chatservices.Models.ModelBuilders;
using Microsoft.EntityFrameworkCore;
using chat_service.Models.ModelBuilders;

namespace chat_service.Db
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
            ReadReceiptBuilder.Build(builder);
            MemberBuilder.Build(builder);
        }
    }
}