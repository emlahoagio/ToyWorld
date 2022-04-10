using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class changeratestarfrominttodouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "NumOfStar",
                table: "RateSeller",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "NumOfStar",
                table: "Rate",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NumOfStar",
                table: "RateSeller",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "NumOfStar",
                table: "Rate",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
