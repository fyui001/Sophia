namespace Sophia.Api.DbContext;

using Microsoft.EntityFrameworkCore;
using Models;

public class SophiaContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<ErrorViewModel> Item { get; set; } = default!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<ErrorViewModel>();
    }
}