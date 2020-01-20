using System;
using Microsoft.EntityFrameworkCore;

namespace realtime_app.Models.ModelBuilders
{
    public class NotificationBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Notification>().ToTable("Notifications");

            entity.HasOne<NotificationType>();
        }
    }
}
