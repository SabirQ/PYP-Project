using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PYP_Project_API.Persistance.Context.Migrations
{
    public partial class createExcelCollectionandExcelDataItemTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExcelCollections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcelCollections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExcelDataItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Segment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Product = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscountBand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitsSold = table.Column<double>(type: "float", nullable: false),
                    ManufacturingPrice = table.Column<double>(type: "float", nullable: false),
                    SalePrice = table.Column<double>(type: "float", nullable: false),
                    GrossSales = table.Column<double>(type: "float", nullable: false),
                    Discounts = table.Column<double>(type: "float", nullable: false),
                    Sales = table.Column<double>(type: "float", nullable: false),
                    COGS = table.Column<double>(type: "float", nullable: false),
                    Profit = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExcelCollectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcelDataItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExcelDataItems_ExcelCollections_ExcelCollectionId",
                        column: x => x.ExcelCollectionId,
                        principalTable: "ExcelCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExcelDataItems_ExcelCollectionId",
                table: "ExcelDataItems",
                column: "ExcelCollectionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExcelDataItems");

            migrationBuilder.DropTable(
                name: "ExcelCollections");
        }
    }
}
