using Microsoft.EntityFrameworkCore;
using Sophia.Infrastructure.Models;

namespace Sophia.Infrastructure.Seeders;

public class UserSeeder
{
    public static void Seed(ModelBuilder modelBuilder) =>
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Auth0Sub = "auth0|test-user-1",
                Name = "山根綺",
                IconUrl = "",
                Status = UserStatus.Valid,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            },
            new User
            {
                Id = 2,
                Auth0Sub = "auth0|test-user-2",
                Name = "桑原由気",
                IconUrl = "",
                Status = UserStatus.Valid,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            }
        );
}
