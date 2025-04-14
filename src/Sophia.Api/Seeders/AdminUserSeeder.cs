using Microsoft.EntityFrameworkCore;
using Sophia.Api.Models;

namespace Sophia.Api.Seeders;

public class AdminUserSeeder
{
    public static void Seed(ModelBuilder modelBuilder) =>
        modelBuilder.Entity<AdminUser>().HasData(
            new AdminUser
            {
                Id = 1,
                Emai = "takada-yuki@new-world.local",
                Name = "高田憂希",
                Role = Role.System,
                Status = AdminUserStatus.Valid,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            }
        );
}