using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sophia.Db.Migrations
{
    /// <inheritdoc />
    public partial class FixSeederTimestamps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 3, 21, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 21, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 3, 21, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 3, 21, 0, 0, 0, 0, DateTimeKind.Utc) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 3, 22, 16, 56, 34, 965, DateTimeKind.Utc).AddTicks(5558), new DateTime(2026, 3, 22, 16, 56, 34, 965, DateTimeKind.Utc).AddTicks(5716) });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "updated_at" },
                values: new object[] { new DateTime(2026, 3, 22, 16, 56, 34, 965, DateTimeKind.Utc).AddTicks(5857), new DateTime(2026, 3, 22, 16, 56, 34, 965, DateTimeKind.Utc).AddTicks(5858) });
        }
    }
}
