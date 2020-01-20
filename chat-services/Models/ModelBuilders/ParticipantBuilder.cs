using Microsoft.EntityFrameworkCore;

namespace realtime_app.Models.ModelBuilders
{
  public class ParticipantBuilder
  {
    public static void Build(ModelBuilder modelBuilder)
    {
      var entity = modelBuilder.Entity<Participant>().ToTable("Participants");
    }
  }
}

