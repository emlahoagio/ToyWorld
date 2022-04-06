using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class updatetableprizeandcotest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanAttempt",
                table: "Contest");

            migrationBuilder.DropColumn(
                name: "IsOnlineContest",
                table: "Contest");

            migrationBuilder.DropColumn(
                name: "RegisterCost",
                table: "Contest");

            migrationBuilder.DropColumn(
                name: "Venue",
                table: "Contest");

            migrationBuilder.AddColumn<bool>(
                name: "IsDisabled",
                table: "Prize",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDisabled",
                table: "Prize");

            migrationBuilder.AddColumn<bool>(
                name: "CanAttempt",
                table: "Contest",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOnlineContest",
                table: "Contest",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "RegisterCost",
                table: "Contest",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Venue",
                table: "Contest",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
