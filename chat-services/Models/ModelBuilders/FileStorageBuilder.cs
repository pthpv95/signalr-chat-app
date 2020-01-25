using Microsoft.EntityFrameworkCore;
using realtime_app.Models;

namespace chat_services.Models.ModelBuilders
{
    public class FileStorageBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<FileStorage>().ToTable("FileStorages");
        }
    }
}