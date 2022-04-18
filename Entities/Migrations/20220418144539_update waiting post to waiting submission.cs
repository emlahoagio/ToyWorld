using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class updatewaitingposttowaitingsubmission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "IsWaiting",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "PublicDate",
                table: "Post");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "PostOfContest",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "PostOfContest");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Post",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Post",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsWaiting",
                table: "Post",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublicDate",
                table: "Post",
                type: "datetime2",
                nullable: true);
        }
    }
}
