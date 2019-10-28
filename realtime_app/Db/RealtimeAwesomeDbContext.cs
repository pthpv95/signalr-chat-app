using Microsoft.EntityFrameworkCore;

namespace realtime_app.Db
{
    public class RealtimeAwesomeDbContext : DbContext
    {
        public RealtimeAwesomeDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}