using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Linnworks.DataSeederUtility.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: true),
                    UpdatedAt = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderPriorities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: true),
                    UpdatedAt = table.Column<string>(nullable: true),
                    OrderPriority = table.Column<string>(type: "NCHAR(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPriorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: true),
                    UpdatedAt = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: true),
                    UpdatedAt = table.Column<string>(nullable: true),
                    OrderedAt = table.Column<DateTime>(type: "DATE", nullable: false),
                    OrderPriorityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_OrderPriorities_OrderPriorityId",
                        column: x => x.OrderPriorityId,
                        principalTable: "OrderPriorities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: true),
                    UpdatedAt = table.Column<string>(nullable: true),
                    RegionId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Countries_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(nullable: true),
                    UpdatedAt = table.Column<string>(nullable: true),
                    OrderId = table.Column<int>(nullable: false),
                    SalesChannel = table.Column<string>(maxLength: 128, nullable: true),
                    ShippedAt = table.Column<DateTime>(type: "DATE", nullable: true),
                    UnitsSold = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<decimal>(type: "DECIMAL(100,2)", nullable: false),
                    UnitCost = table.Column<decimal>(type: "DECIMAL(100,2)", nullable: false),
                    TotalRevenue = table.Column<decimal>(type: "DECIMAL(100,2)", nullable: false),
                    TotalCost = table.Column<decimal>(type: "DECIMAL(100,2)", nullable: false),
                    TotalProfit = table.Column<decimal>(type: "DECIMAL(100,2)", nullable: false),
                    ItemId = table.Column<int>(nullable: true),
                    CountryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sales_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sales_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Countries_RegionId",
                table: "Countries",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPriorities_OrderPriority",
                table: "OrderPriorities",
                column: "OrderPriority",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderPriorityId",
                table: "Orders",
                column: "OrderPriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CountryId",
                table: "Sales",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ItemId",
                table: "Sales",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_OrderId",
                table: "Sales",
                column: "OrderId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "OrderPriorities");
        }
    }
}
