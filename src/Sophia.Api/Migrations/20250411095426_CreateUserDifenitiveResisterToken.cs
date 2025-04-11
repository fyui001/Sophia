using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sophia.Api.Migrations
{
    /// <inheritdoc />
    public partial class CreateUserDifenitiveResisterToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "ix_user_definitive_register_token_user_id",
                table: "user_definitive_register_token",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_definitive_register_token");
        }
    }
}
