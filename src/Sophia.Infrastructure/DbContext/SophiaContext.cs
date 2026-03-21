namespace Sophia.Infrastructure.DbContext;

using Microsoft.EntityFrameworkCore;
using Models;
using Sophia.Infrastructure.Seeders;

public class SophiaContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>();

        UserSeeder.Seed(modelBuilder);
    }
}
