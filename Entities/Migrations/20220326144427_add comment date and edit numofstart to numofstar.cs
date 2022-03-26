using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class addcommentdateandeditnumofstarttonumofstar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumOfStart",
                table: "Rate",
                newName: "NumOfStar");

            migrationBuilder.AddColumn<DateTime>(
                name: "CommentDate",
                table: "Comment",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.UtcNow);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentDate",
                table: "Comment");

            migrationBuilder.RenameColumn(
                name: "NumOfStar",
                table: "Rate",
                newName: "NumOfStart");
        }
    }
}
