using Microsoft.EntityFrameworkCore;

namespace chat_service.Models.ModelBuilders
{
    public class UserBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}