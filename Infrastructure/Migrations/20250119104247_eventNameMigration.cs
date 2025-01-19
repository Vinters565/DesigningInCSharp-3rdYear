using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchedulePlanner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class eventNameMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CalendarEvents",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "CalendarEvents");
        }
    }
}
