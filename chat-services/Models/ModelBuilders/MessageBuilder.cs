using Microsoft.EntityFrameworkCore;

namespace realtime_app.Models.ModelBuilders
{
    public class MessageBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Message>().ToTable("Messages");

            entity.HasMany(m => m.ReadReceipts)
                .WithOne(x => x.Message)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(x => x.Conversation)
                .WithMany(x => x.Messages);
        }
    }
}