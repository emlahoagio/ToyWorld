using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class addbillidtoimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BillId",
                table: "Image",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Bill",
                table: "Image",
                column: "BillId",
                principalTable: "Bill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Bill",
                table: "Image");


            migrationBuilder.DropColumn(
                name: "BillId",
                table: "Image");
        }
    }
}
