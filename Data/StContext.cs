using Microsoft.EntityFrameworkCore;

namespace SmokeTracker.Data;

public class StContext : DbContext
{
    public DbSet<Log> Logs { get; set; }
    public StContext(DbContextOptions<StContext> options) : base(options) { }
}