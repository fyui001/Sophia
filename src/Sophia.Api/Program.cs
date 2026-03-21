using DotNetEnv;
using Sophia.Api;
using Sophia.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

builder.Services.AddDbContextPool<SophiaContext>(options =>
    options.UseMySql(
        connectionString,
        new MySqlServerVersion(new Version(9, 3, 0)),
        b => b.MigrationsAssembly("Sophia.Db")
    ).UseSnakeCaseNamingConvention()
);

builder.AddApplicationBuilder();

var app = builder.Build();
app.UseWebApplication().Run();

public partial class Program { }
