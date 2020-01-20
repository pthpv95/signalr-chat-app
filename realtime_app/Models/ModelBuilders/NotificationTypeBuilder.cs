using System;
using Microsoft.EntityFrameworkCore;

namespace realtime_app.Models.ModelBuilders
{
    public class NotificationTypeBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<NotificationType>().ToTable("NotificationTypes");
        }
    }
}
