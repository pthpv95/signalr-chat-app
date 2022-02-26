using System;
using Microsoft.EntityFrameworkCore;

namespace chat_service.Models.ModelBuilders
{
    public class NotificationTypeBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<NotificationType>().ToTable("NotificationTypes");
        }
    }
}
