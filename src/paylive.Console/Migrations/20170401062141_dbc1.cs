using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace paylive.Console.Migrations
{
    public partial class dbc1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateBody",
                table: "WebImConfig",
                newName: "UserName");

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

            migrationBuilder.CreateTable(
                name: "SmsQu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    AddTime = table.Column<DateTime>(nullable: false),
                    Completed = table.Column<bool>(nullable: false),
                    Msg = table.Column<string>(nullable: true),
                    Receivers = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmsQu", x => x.Id);
                });

            migrationBuilder.AddColumn<string>(
                name: "Ssid",
                table: "WebImConfig",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "WebImConfig",
                newName: "DateBody");

            migrationBuilder.DropColumn(
                name: "Ssid",
                table: "WebImConfig");

            migrationBuilder.DropTable(
                name: "Receivers");

            migrationBuilder.DropTable(
                name: "SmsQu");
        }
    }
}
