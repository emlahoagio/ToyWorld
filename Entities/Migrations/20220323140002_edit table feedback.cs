using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class edittablefeedback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostOfContestId",
                table: "Feedback",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_PostOfContest",
                table: "Feedback",
                column: "PostOfContestId",
                principalTable: "PostOfContest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_TradingPost",
                table: "Feedback",
                column: "TradingPostId",
                principalTable: "TradingPost",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_PostOfContest",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_TradingPost",
                table: "Feedback");

            migrationBuilder.DropColumn(
                name: "PostOfContestId",
                table: "Feedback");
        }
    }
}
