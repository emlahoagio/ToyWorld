using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class removecolummcontestidinimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Contest",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "ContestId",
                table: "Image");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContestId",
                table: "Image",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Contest",
                table: "Image",
                column: "ContestId",
                principalTable: "Contest",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
