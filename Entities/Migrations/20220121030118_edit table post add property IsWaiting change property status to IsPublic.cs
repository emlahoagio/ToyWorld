using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class edittablepostaddpropertyIsWaitingchangepropertystatustoIsPublic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Post",
                newName: "IsWaiting");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Post",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Post");

            migrationBuilder.RenameColumn(
                name: "IsWaiting",
                table: "Post",
                newName: "Status");
        }
    }
}
