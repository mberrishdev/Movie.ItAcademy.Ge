using Microsoft.EntityFrameworkCore.Migrations;

namespace Movie.Persistance.Migrations
{
    public partial class ChangeTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ArchiveLog",
                schema: "log",
                table: "ArchiveLog");

            migrationBuilder.RenameTable(
                name: "ArchiveLog",
                schema: "log",
                newName: "ArchiveLogs",
                newSchema: "log");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArchiveLogs",
                schema: "log",
                table: "ArchiveLogs",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ArchiveLogs",
                schema: "log",
                table: "ArchiveLogs");

            migrationBuilder.RenameTable(
                name: "ArchiveLogs",
                schema: "log",
                newName: "ArchiveLog",
                newSchema: "log");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArchiveLog",
                schema: "log",
                table: "ArchiveLog",
                column: "Id");
        }
    }
}
