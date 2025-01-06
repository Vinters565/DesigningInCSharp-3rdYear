using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchedulePlanner.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class userSettingsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DisplayedName",
                table: "Users",
                newName: "Settings_DisplayedName");

            migrationBuilder.AddColumn<string>(
                name: "Settings_PrimaryColor",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Settings_SecondaryColor",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Settings_PrimaryColor",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Settings_SecondaryColor",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Settings_DisplayedName",
                table: "Users",
                newName: "DisplayedName");
        }
    }
}
