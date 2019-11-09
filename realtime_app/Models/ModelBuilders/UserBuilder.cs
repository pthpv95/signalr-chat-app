using Microsoft.EntityFrameworkCore;

namespace realtime_app.Models.ModelBuilders
{
    public class UserBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<User>();

            entity.HasMany(x => x.UserMessages)
                .WithOne(x => x.User);
        }
    }
}