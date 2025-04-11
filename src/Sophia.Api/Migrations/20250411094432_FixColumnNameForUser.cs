using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sophia.Api.Migrations
{
    /// <inheritdoc />
    public partial class FixColumnNameForUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "users",
                newName: "discord_user_id");

            migrationBuilder.RenameIndex(
                name: "ix_users_user_id",
                table: "users",
                newName: "ix_users_discord_user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "discord_user_id",
                table: "users",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "ix_users_discord_user_id",
                table: "users",
                newName: "ix_users_user_id");
        }
    }
}
