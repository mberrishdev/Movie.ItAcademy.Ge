using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Movie.Persistance.Migrations
{
    public partial class AddLogTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "log");

            migrationBuilder.CreateTable(
                name: "APIWebLogs",
                schema: "log",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime", nullable: false),
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogEvent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APIWebLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BOWebLogs",
                schema: "log",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime", nullable: false),
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogEvent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BOWebLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MVCWebLogs",
                schema: "log",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime", nullable: false),
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogEvent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MVCWebLogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APIWebLogs",
                schema: "log");

            migrationBuilder.DropTable(
                name: "BOWebLogs",
                schema: "log");

            migrationBuilder.DropTable(
                name: "MVCWebLogs",
                schema: "log");
        }
    }
}
