using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BFS_backend.Migrations
{
    public partial class changingdatetype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "month",
                table: "MonthlyStatements");

            migrationBuilder.AlterColumn<double>(
                name: "income",
                table: "MonthlyStatements",
                type: "float",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<double>(
                name: "expenses",
                table: "MonthlyStatements",
                type: "float",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<double>(
                name: "currentBalance",
                table: "MonthlyStatements",
                type: "float",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<double>(
                name: "balance",
                table: "MonthlyStatements",
                type: "float",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<DateTime>(
                name: "monthYear",
                table: "MonthlyStatements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "monthYear",
                table: "MonthlyStatements");

            migrationBuilder.AlterColumn<long>(
                name: "income",
                table: "MonthlyStatements",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<long>(
                name: "expenses",
                table: "MonthlyStatements",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<long>(
                name: "currentBalance",
                table: "MonthlyStatements",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<long>(
                name: "balance",
                table: "MonthlyStatements",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "month",
                table: "MonthlyStatements",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
