using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class edittablepostofcontest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Rate",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreate",
                table: "PostOfContest",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RateContest_Account",
                table: "Rate",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RateContest_Account",
                table: "Rate");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Rate");

            migrationBuilder.DropColumn(
                name: "DateCreate",
                table: "PostOfContest");
        }
    }
}
