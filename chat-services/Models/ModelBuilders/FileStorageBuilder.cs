using Microsoft.EntityFrameworkCore;
using chat_service.Models;

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