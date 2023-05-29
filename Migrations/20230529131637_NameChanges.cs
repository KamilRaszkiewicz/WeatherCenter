using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherCenter.Migrations
{
    /// <inheritdoc />
    public partial class NameChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Widgets",
                newName: "Region");

            migrationBuilder.RenameColumn(
                name: "CountryCode",
                table: "Widgets",
                newName: "Country");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Region",
                table: "Widgets",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Widgets",
                newName: "CountryCode");
        }
    }
}
