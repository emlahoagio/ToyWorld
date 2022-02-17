using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class addcolummIsWaitingtotableproposal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsWaiting",
                table: "Proposal",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsWaiting",
                table: "Proposal");
        }
    }
}
