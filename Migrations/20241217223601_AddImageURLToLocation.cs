using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace T2HackathonCase2.Migrations
{
    /// <inheritdoc />
    public partial class AddImageURLToLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Locations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Locations");
        }
    }
}
