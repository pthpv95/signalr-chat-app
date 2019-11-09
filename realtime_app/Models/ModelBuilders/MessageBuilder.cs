using Microsoft.EntityFrameworkCore;

namespace realtime_app.Models.ModelBuilders
{
    public class MessageBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Message>().ToTable("Messages");
            entity.HasMany(x => x.UserMessage)
                .WithOne(x => x.Message);
        }
    }
}