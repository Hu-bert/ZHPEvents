using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ZHPEvents.Data.Migrations
{
    public partial class report : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tittle",
                table: "Event",
                newName: "Title");

            migrationBuilder.CreateTable(
                name: "Raport",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 60, nullable: false),
                    AdditionTime = table.Column<DateTime>(nullable: false),
                    AddingPerson = table.Column<string>(nullable: true),
                    ConfirmingPerson = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raport", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Raport");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Event",
                newName: "Tittle");
        }
    }
}
