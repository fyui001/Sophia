namespace Sophia.Db;

using Sophia.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class SophiaDbContextFactory : IDesignTimeDbContextFactory<SophiaContext>
{
    public SophiaContext CreateDbContext(string[] args)
    {
        var basePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../Sophia.Api"));

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<SophiaContext>();
        optionsBuilder.UseNpgsql(
            connectionString,
            b => b.MigrationsAssembly("Sophia.Db")
        ).UseSnakeCaseNamingConvention();

        return new SophiaContext(optionsBuilder.Options);
    }
}
