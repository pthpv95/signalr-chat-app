using Microsoft.EntityFrameworkCore;

namespace chat_service.Models.ModelBuilders
{
    public class UserContactBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<UserContact>().ToTable("UserContacts");

            entity.HasKey(uc => new { uc.UserId, uc.ContactId });

            entity.HasOne<User>(uc => uc.User)
                .WithMany(uc => uc.UserContacts)
                .HasForeignKey(uc => uc.UserId);

            entity.HasOne(uc => uc.Contact)
                .WithMany(uc => uc.UserContacts)
                .HasForeignKey(uc => uc.ContactId);
        }
    }
}