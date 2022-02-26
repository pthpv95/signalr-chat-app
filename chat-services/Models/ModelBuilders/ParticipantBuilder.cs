using Microsoft.EntityFrameworkCore;

namespace chat_service.Models.ModelBuilders
{
  public class ParticipantBuilder
  {
    public static void Build(ModelBuilder modelBuilder)
    {
      var entity = modelBuilder.Entity<Participant>().ToTable("Participants");
    }
  }
}

