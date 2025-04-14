using Microsoft.EntityFrameworkCore;
using Sophia.Api.Models;

namespace Sophia.Api.Seeders;

public class UserSeeder
{
    public static void Seed(ModelBuilder modelBuilder) =>
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Emai = "aya-yamane@new-world.local",
                Name = "山根綺",
                IconUrl = "",
                Status = UserStatus.Valid,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            },
            new User
            {
                Id = 2,
                Emai = "kuwahara-yukinew-world.local",
                Name = "桑原由気",
                IconUrl = "",
                Status = UserStatus.Valid,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            }
        );
}