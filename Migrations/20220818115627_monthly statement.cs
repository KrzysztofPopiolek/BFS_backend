using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BFS_backend.Migrations
{
    public partial class monthlystatement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "mileage",
                table: "MileageRecords",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecordDate",
                table: "MileageRecords",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "MonthlyStatements",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    month = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    income = table.Column<long>(type: "bigint", nullable: false),
                    expenses = table.Column<long>(type: "bigint", nullable: false),
                    balance = table.Column<long>(type: "bigint", nullable: false),
                    currentBalance = table.Column<long>(type: "bigint", nullable: false),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyStatements", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonthlyStatements");

            migrationBuilder.AlterColumn<long>(
                name: "mileage",
                table: "MileageRecords",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "RecordDate",
                table: "MileageRecords",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
