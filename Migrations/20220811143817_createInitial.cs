using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BFS_backend.Migrations
{
    public partial class createInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessOwnerDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    businessName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    businessAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    businessUTR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    businessEstablishmentDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ownerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ownerNin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ownerHmrcId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ownerContact = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessOwnerDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractorDetailsConsts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EvidenceNumberName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractorAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorDetailsConsts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EvidenceNumberName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractorAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransferType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventValue = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MileageRecords",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastOdometerReading = table.Column<long>(type: "bigint", nullable: false),
                    FinishOdometerReading = table.Column<long>(type: "bigint", nullable: false),
                    MilesOutOfService = table.Column<long>(type: "bigint", nullable: true),
                    mileage = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MileageRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxRates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bandName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    taxableValue = table.Column<double>(type: "float", nullable: false),
                    taxRate = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxRates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxYearDatesDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    taxYearStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    taxYearEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    selfAssessmentDeadline = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxYearDatesDetails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessOwnerDetails");

            migrationBuilder.DropTable(
                name: "ContractorDetailsConsts");

            migrationBuilder.DropTable(
                name: "EventDetails");

            migrationBuilder.DropTable(
                name: "MileageRecords");

            migrationBuilder.DropTable(
                name: "TaxRates");

            migrationBuilder.DropTable(
                name: "TaxYearDatesDetails");
        }
    }
}
