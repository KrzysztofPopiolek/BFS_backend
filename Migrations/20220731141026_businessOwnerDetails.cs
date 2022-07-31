using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BFS_backend.Migrations
{
    public partial class businessOwnerDetails : Migration
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessOwnerDetails");
        }
    }
}
