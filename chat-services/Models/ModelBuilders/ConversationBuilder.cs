using Microsoft.EntityFrameworkCore;

namespace realtime_app.Models.ModelBuilders
{
  public class ConversationBuilder
  {
    public static void Build(ModelBuilder modelBuilder)
    {
      var entity = modelBuilder.Entity<Conversation>().ToTable("Conversations");
    }
  }
}