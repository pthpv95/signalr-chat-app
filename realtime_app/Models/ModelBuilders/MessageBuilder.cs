using Microsoft.EntityFrameworkCore;

namespace realtime_app.Models.ModelBuilders
{
    public class MessageBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Message>().ToTable("Messages");
        }
    }
}