using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class edittableproposal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TakePlace",
                table: "Proposal");

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Proposal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Proposal",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Proposal");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Proposal");

            migrationBuilder.AddColumn<int>(
                name: "TakePlace",
                table: "Proposal",
                type: "int",
                nullable: true);
        }
    }
}
