using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchingApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BirthTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    BirthLocation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientAId = table.Column<int>(type: "int", nullable: false),
                    ClientBId = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NatalCharts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    SunSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MoonSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MercurySign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VenusSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarsSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JupiterSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaturnSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UranusSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NeptuneSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlutoSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ascendant = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NatalCharts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NatalCharts_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Email",
                table: "Clients",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_ClientAId_ClientBId",
                table: "Matches",
                columns: new[] { "ClientAId", "ClientBId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NatalCharts_ClientId",
                table: "NatalCharts",
                column: "ClientId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "NatalCharts");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
