using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sophia.Api.Migrations
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
                name: "admin_user",
                columns: table => new
                {
                    id = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    emai = table.Column<string>(type: "varchar(255)", nullable: true, comment: "メールアドレス")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(255)", nullable: false, comment: "名前")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    role = table.Column<string>(type: "varchar(255)", nullable: false, comment: "ロール")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<string>(type: "varchar(255)", nullable: false, comment: "ステータス")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "作成日時"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "更新日時")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_admin_user", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_definitive_register_token",
                columns: table => new
                {
                    id = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    token = table.Column<string>(type: "longtext", nullable: false, comment: "トークン")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_verified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    expired_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "有効期限"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "作成日時"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "更新日時")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_definitive_register_token", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    emai = table.Column<string>(type: "varchar(255)", nullable: true, comment: "メールアドレス")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(255)", nullable: false, comment: "名前")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    icon_url = table.Column<string>(type: "varchar(255)", nullable: false, comment: "アイコンURL")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<string>(type: "varchar(255)", nullable: false, comment: "ステータス")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "作成日時"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "更新日時")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "admin_user",
                columns: new[] { "id", "created_at", "emai", "name", "role", "status", "updated_at" },
                values: new object[] { 1ul, new DateTime(2025, 4, 14, 4, 26, 51, 523, DateTimeKind.Utc).AddTicks(3664), "takada-yuki@new-world.local", "高田憂希", "System", "Valid", new DateTime(2025, 4, 14, 4, 26, 51, 523, DateTimeKind.Utc).AddTicks(3896) });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "emai", "icon_url", "name", "status", "updated_at" },
                values: new object[,]
                {
                    { 1ul, new DateTime(2025, 4, 14, 4, 26, 51, 524, DateTimeKind.Utc).AddTicks(1915), "aya-yamane@new-world.local", "", "山根綺", "Valid", new DateTime(2025, 4, 14, 4, 26, 51, 524, DateTimeKind.Utc).AddTicks(2078) },
                    { 2ul, new DateTime(2025, 4, 14, 4, 26, 51, 524, DateTimeKind.Utc).AddTicks(2217), "kuwahara-yukinew-world.local", "", "桑原由気", "Valid", new DateTime(2025, 4, 14, 4, 26, 51, 524, DateTimeKind.Utc).AddTicks(2217) }
                });

            migrationBuilder.CreateIndex(
                name: "ix_user_definitive_register_token_user_id",
                table: "user_definitive_register_token",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_emai",
                table: "users",
                column: "emai",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin_user");

            migrationBuilder.DropTable(
                name: "user_definitive_register_token");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
