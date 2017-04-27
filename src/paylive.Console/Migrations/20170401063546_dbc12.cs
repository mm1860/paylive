using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace paylive.Console.Migrations
{
    public partial class dbc12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Receivers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Receivers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    AddTime = table.Column<DateTime>(nullable: false),
                    ImNo = table.Column<int>(nullable: false),
                    Mobile = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receivers", x => x.Id);
                });
        }
    }
}
