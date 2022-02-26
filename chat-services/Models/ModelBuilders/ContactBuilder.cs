using Microsoft.EntityFrameworkCore;

namespace chat_service.Models.ModelBuilders
{
    public class ContactBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Contact>().ToTable("Contacts");
        }
    }
}