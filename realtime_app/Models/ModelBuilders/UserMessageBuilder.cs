using Microsoft.EntityFrameworkCore;

namespace realtime_app.Models.ModelBuilders
{
    public class UserMessageBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<UserMessage>();

            entity.HasKey(x => new { x.MessageId, x.UserId });

            entity.HasOne(x => x.Message)
                .WithMany(x => x.UserMessage)
                .HasForeignKey(x => x.MessageId);

            entity.HasOne(x => x.User)
                .WithMany(x => x.UserMessages)
                .HasForeignKey(x => x.UserId);
        }
    }
}