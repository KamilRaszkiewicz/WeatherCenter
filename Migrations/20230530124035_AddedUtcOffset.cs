using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherCenter.Migrations
{
    /// <inheritdoc />
    public partial class AddedUtcOffset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UtcOffset",
                table: "Widgets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UtcOffset",
                table: "Widgets");
        }
    }
}
