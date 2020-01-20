using Microsoft.EntityFrameworkCore;

namespace realtime_app.Models.ModelBuilders
{
    public class ContactBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Contact>().ToTable("Contacts");
        }
    }
}