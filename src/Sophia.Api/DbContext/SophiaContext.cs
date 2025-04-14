using Sophia.Api.Seeders;

namespace Sophia.Api.DbContext;

using Microsoft.EntityFrameworkCore;
using Models;

public class SophiaContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserDefinitiveRegisterToken> UserDefinitiveRegisterToken { get; set; } = null!;
    public DbSet<AdminUser> AdminUser { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        
        AdminUserSeeder.Seed(modelBuilder);
        UserSeeder.Seed(modelBuilder);
    }
}