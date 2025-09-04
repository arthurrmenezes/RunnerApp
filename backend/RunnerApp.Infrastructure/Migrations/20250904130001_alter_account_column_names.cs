using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunnerApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class alter_account_column_names : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "accounts",
                newName: "surname");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "accounts",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "accounts",
                newName: "first_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "surname",
                table: "accounts",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "accounts",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "accounts",
                newName: "FirstName");
        }
    }
}
