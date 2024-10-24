using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mebil2.Migrations
{
    /// <inheritdoc />
    public partial class innit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bankDetails = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Factories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "FurnitureInOrders",
                columns: table => new
                {
                    orderId = table.Column<int>(type: "int", nullable: false),
                    productId = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customerId = table.Column<int>(type: "int", nullable: false),
                    orderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    totalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    orderStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_customerId",
                        column: x => x.customerId,
                        principalTable: "Customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Furnitures",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    factoryId = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    furnitureType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    currentPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Furnitures", x => x.id);
                    table.ForeignKey(
                        name: "FK_Furnitures_Factories_factoryId",
                        column: x => x.factoryId,
                        principalTable: "Factories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FurnitureInWarehouses",
                columns: table => new
                {
                    furnitureId = table.Column<int>(type: "int", nullable: false),
                    warehouseId = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_FurnitureInWarehouses_Furnitures_furnitureId",
                        column: x => x.furnitureId,
                        principalTable: "Furnitures",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FurnitureInWarehouses_Warehouses_warehouseId",
                        column: x => x.warehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    orderId = table.Column<int>(type: "int", nullable: false),
                    furnitureId = table.Column<int>(type: "int", nullable: false),
                    warehouseId = table.Column<int>(type: "int", nullable: false),
                    totalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    furnitureCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.id);
                    table.ForeignKey(
                        name: "FK_Invoices_Furnitures_furnitureId",
                        column: x => x.furnitureId,
                        principalTable: "Furnitures",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Orders_orderId",
                        column: x => x.orderId,
                        principalTable: "Orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FurnitureInWarehouses_furnitureId",
                table: "FurnitureInWarehouses",
                column: "furnitureId");

            migrationBuilder.CreateIndex(
                name: "IX_FurnitureInWarehouses_warehouseId",
                table: "FurnitureInWarehouses",
                column: "warehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_Furnitures_factoryId",
                table: "Furnitures",
                column: "factoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_furnitureId",
                table: "Invoices",
                column: "furnitureId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_orderId",
                table: "Invoices",
                column: "orderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_customerId",
                table: "Orders",
                column: "customerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FurnitureInOrders");

            migrationBuilder.DropTable(
                name: "FurnitureInWarehouses");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Furnitures");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Factories");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
