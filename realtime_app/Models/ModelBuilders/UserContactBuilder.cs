using Microsoft.EntityFrameworkCore;

namespace realtime_app.Models.ModelBuilders
{
    public class UserContactBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<UserContact>().ToTable("UserContacts");
        }
    }
}