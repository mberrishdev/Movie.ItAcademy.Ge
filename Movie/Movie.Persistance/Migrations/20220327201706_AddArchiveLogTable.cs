using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Movie.Persistance.Migrations
{
    public partial class AddArchiveLogTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArchiveLog",
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
                    table.PrimaryKey("PK_ArchiveLog", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArchiveLog",
                schema: "log");
        }
    }
}
