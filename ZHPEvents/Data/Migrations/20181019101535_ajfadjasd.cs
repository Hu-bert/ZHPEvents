using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZHPEvents.Data.Migrations
{
    public partial class ajfadjasd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AdditionTime",
                table: "Event",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AdditionTime",
                table: "Event",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
