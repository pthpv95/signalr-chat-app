using Microsoft.EntityFrameworkCore;
using realtime_app.Models.ModelBuilders;

namespace realtime_app.Db
{
    public class RealtimeAwesomeDbContext : DbContext
    {
        public RealtimeAwesomeDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            MessageBuilder.Build(builder);
            UserBuilder.Build(builder);
            UserMessageBuilder.Build(builder);
        }
    }
}