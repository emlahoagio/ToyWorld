using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class addtablepostofcontestandrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostOfContestId",
                table: "Image",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsOnlineContest",
                table: "Contest",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "RegistrationCost",
                table: "Contest",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Contest",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PostOfContest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContestId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostOfContest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostOfContest_Contest",
                        column: x => x.ContestId,
                        principalTable: "Contest",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumOfStart = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostOfContestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rate_PostOfContest",
                        column: x => x.PostOfContestId,
                        principalTable: "PostOfContest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Image_PostOfContest",
                table: "Image",
                column: "PostOfContestId",
                principalTable: "PostOfContest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_PostOfContest_PostOfContestId",
                table: "Image");

            migrationBuilder.DropTable(
                name: "Rate");

            migrationBuilder.DropTable(
                name: "PostOfContest");

            migrationBuilder.DropColumn(
                name: "PostOfContestId",
                table: "Image");

            migrationBuilder.DropColumn(
                name: "IsOnlineContest",
                table: "Contest");

            migrationBuilder.DropColumn(
                name: "RegistrationCost",
                table: "Contest");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Contest");
        }
    }
}
