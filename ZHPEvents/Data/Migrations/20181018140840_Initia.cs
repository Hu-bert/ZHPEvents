using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZHPEvents.Data.Migrations
{
    public partial class Initia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "Contact",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "ForWhom",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "LastDayOfEntries",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "NumberOfSeats",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "Organizaer",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "Poster",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "RegistrationMail",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Event");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Event",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Contact",
                table: "Event",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Event",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Event",
                maxLength: 60,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ForWhom",
                table: "Event",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastDayOfEntries",
                table: "Event",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "NumberOfSeats",
                table: "Event",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Organizaer",
                table: "Event",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Poster",
                table: "Event",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegistrationMail",
                table: "Event",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Thumbnail",
                table: "Event",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Event",
                nullable: false,
                defaultValue: 0);
        }
    }
}
