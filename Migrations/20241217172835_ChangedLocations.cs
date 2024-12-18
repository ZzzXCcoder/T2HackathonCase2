using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T2HackathonCase2.Migrations
{
    /// <inheritdoc />
    public partial class ChangedLocations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "OpeningHours");

            migrationBuilder.RenameColumn(
                name: "OpeningTime",
                table: "OpeningHours",
                newName: "Start");

            migrationBuilder.RenameColumn(
                name: "ClosingTime",
                table: "OpeningHours",
                newName: "Recurence");

            migrationBuilder.AddColumn<string>(
                name: "Duration",
                table: "OpeningHours",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "OpeningHours");

            migrationBuilder.RenameColumn(
                name: "Start",
                table: "OpeningHours",
                newName: "OpeningTime");

            migrationBuilder.RenameColumn(
                name: "Recurence",
                table: "OpeningHours",
                newName: "ClosingTime");

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "OpeningHours",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
