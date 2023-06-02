using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherCenter.Migrations
{
    /// <inheritdoc />
    public partial class AddedFullNameColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullLocation",
                table: "Widgets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullLocation",
                table: "Widgets");
        }
    }
}
