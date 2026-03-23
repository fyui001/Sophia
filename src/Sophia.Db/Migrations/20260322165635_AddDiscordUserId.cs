using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sophia.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscordUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "discord_user_id",
                table: "users",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true,
                comment: "Discord User ID")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "discord_user_id", "updated_at" },
                values: new object[] { new DateTime(2026, 3, 22, 16, 56, 34, 965, DateTimeKind.Utc).AddTicks(5558), null, new DateTime(2026, 3, 22, 16, 56, 34, 965, DateTimeKind.Utc).AddTicks(5716) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "discord_user_id", "updated_at" },
                values: new object[] { new DateTime(2026, 3, 22, 16, 56, 34, 965, DateTimeKind.Utc).AddTicks(5857), null, new DateTime(2026, 3, 22, 16, 56, 34, 965, DateTimeKind.Utc).AddTicks(5858) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "discord_user_id",
                table: "users");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 3, 21, 4, 21, 56, 818, DateTimeKind.Utc).AddTicks(6040), new DateTime(2026, 3, 21, 4, 21, 56, 818, DateTimeKind.Utc).AddTicks(6303) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 3, 21, 4, 21, 56, 818, DateTimeKind.Utc).AddTicks(6554), new DateTime(2026, 3, 21, 4, 21, 56, 818, DateTimeKind.Utc).AddTicks(6554) });
        }
    }
}
