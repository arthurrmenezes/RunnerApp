using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunnerApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_table_training_on_database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "trainings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    location = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    distance = table.Column<double>(type: "double precision", nullable: false),
                    duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trainings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "trainings");
        }
    }
}
