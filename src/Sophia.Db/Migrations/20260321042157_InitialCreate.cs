using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sophia.Db.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    auth0sub = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "Auth0 sub")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "名前")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    icon_url = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "アイコンURL")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<int>(type: "int", maxLength: 255, nullable: false, comment: "ステータス"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "作成日時"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "更新日時")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "auth0sub", "created_at", "icon_url", "name", "status", "updated_at" },
                values: new object[,]
                {
                    { 1L, "auth0|test-user-1", new DateTime(2026, 3, 21, 4, 21, 56, 818, DateTimeKind.Utc).AddTicks(6040), "", "山根綺", 0, new DateTime(2026, 3, 21, 4, 21, 56, 818, DateTimeKind.Utc).AddTicks(6303) },
                    { 2L, "auth0|test-user-2", new DateTime(2026, 3, 21, 4, 21, 56, 818, DateTimeKind.Utc).AddTicks(6554), "", "桑原由気", 0, new DateTime(2026, 3, 21, 4, 21, 56, 818, DateTimeKind.Utc).AddTicks(6554) }
                });

            migrationBuilder.CreateIndex(
                name: "ix_users_auth0sub",
                table: "users",
                column: "auth0sub",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
