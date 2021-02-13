using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Data
{
  public class MyKeysContext : DbContext, IDataProtectionKeyContext
  {
    // A recommended constructor overload when using EF Core 
    // with dependency injection.
    public MyKeysContext(DbContextOptions<MyKeysContext> options)
        : base(options) { }

    // This maps to the table that stores keys.
    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
  }
}