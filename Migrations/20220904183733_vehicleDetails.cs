using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BFS_backend.Migrations
{
    public partial class vehicleDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MonthlyStatements",
                table: "MonthlyStatements");

            migrationBuilder.RenameTable(
                name: "MonthlyStatements",
                newName: "MonthlyStatement");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MonthlyStatement",
                table: "MonthlyStatement",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MonthlyStatement",
                table: "MonthlyStatement");

            migrationBuilder.RenameTable(
                name: "MonthlyStatement",
                newName: "MonthlyStatements");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MonthlyStatements",
                table: "MonthlyStatements",
                column: "Id");
        }
    }
}
