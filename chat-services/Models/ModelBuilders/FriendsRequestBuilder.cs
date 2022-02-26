using Microsoft.EntityFrameworkCore;

namespace chat_service.Models.ModelBuilders
{
    public class FriendsRequestBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<FriendsRequest>().ToTable("FriendsRequests");
        }
    }
}