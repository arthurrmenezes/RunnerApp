using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunnerApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_table_account_on_database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "trainings",
                type: "uuid",
                nullable: false);

            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Surname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_trainings_AccountId",
                table: "trainings",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_trainings_accounts_AccountId",
                table: "trainings",
                column: "AccountId",
                principalTable: "accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_trainings_accounts_AccountId",
                table: "trainings");

            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropIndex(
                name: "IX_trainings_AccountId",
                table: "trainings");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "trainings");
        }
    }
}
