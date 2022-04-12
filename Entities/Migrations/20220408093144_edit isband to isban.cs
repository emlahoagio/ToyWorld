using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class editisbandtoisban : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
            name: "IsBand",
            table: "JoinedToContest",
            newName: "IsBan");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
            name: "IsBan",
            table: "JoinedToContest",
            newName: "IsBand");
        }
    }
}
