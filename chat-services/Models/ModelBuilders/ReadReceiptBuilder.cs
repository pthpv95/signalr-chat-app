using Microsoft.EntityFrameworkCore;

namespace chatservices.Models.ModelBuilders
{
    public class ReadReceiptBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<ReadReceipt>().ToTable("ReadReceipts");
            entity.HasKey(x => new { x.MessageId, x.SeenerId });
            entity.HasOne(x => x.Message).WithMany(x => x.ReadReceipts);
        }
    }
}
