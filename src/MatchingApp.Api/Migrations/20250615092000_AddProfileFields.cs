using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchingApp.Api.Migrations
{
    public partial class AddProfileFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Interests",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsProfilePublic",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePhotoFileName",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhotoApproved",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bio",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Interests",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "IsProfilePublic",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ProfilePhotoFileName",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "PhotoApproved",
                table: "Clients");
        }
    }
}
