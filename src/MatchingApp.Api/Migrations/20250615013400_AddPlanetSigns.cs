using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchingApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddPlanetSigns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MercurySign",
                table: "NatalCharts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VenusSign",
                table: "NatalCharts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MarsSign",
                table: "NatalCharts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JupiterSign",
                table: "NatalCharts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SaturnSign",
                table: "NatalCharts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UranusSign",
                table: "NatalCharts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NeptuneSign",
                table: "NatalCharts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlutoSign",
                table: "NatalCharts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MercurySign",
                table: "NatalCharts");

            migrationBuilder.DropColumn(
                name: "VenusSign",
                table: "NatalCharts");

            migrationBuilder.DropColumn(
                name: "MarsSign",
                table: "NatalCharts");

            migrationBuilder.DropColumn(
                name: "JupiterSign",
                table: "NatalCharts");

            migrationBuilder.DropColumn(
                name: "SaturnSign",
                table: "NatalCharts");

            migrationBuilder.DropColumn(
                name: "UranusSign",
                table: "NatalCharts");

            migrationBuilder.DropColumn(
                name: "NeptuneSign",
                table: "NatalCharts");

            migrationBuilder.DropColumn(
                name: "PlutoSign",
                table: "NatalCharts");
        }
    }
}
