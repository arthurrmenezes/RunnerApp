using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunnerApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_profilepictureurl_to_account : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "profile_picture_url",
                table: "accounts",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "profile_picture_url",
                table: "accounts");
        }
    }
}
