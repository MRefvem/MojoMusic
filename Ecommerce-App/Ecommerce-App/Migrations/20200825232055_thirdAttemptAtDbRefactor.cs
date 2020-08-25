using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce_App.Migrations
{
    public partial class thirdAttemptAtDbRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_OrderItems_OrderItemsOrderId_OrderItemsProductId",
                table: "Carts");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_Carts_OrderItemsOrderId_OrderItemsProductId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "OrderItemsOrderId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "OrderItemsProductId",
                table: "Carts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderItemsOrderId",
                table: "Carts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderItemsProductId",
                table: "Carts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderItems_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carts_OrderItemsOrderId_OrderItemsProductId",
                table: "Carts",
                columns: new[] { "OrderItemsOrderId", "OrderItemsProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_OrderItems_OrderItemsOrderId_OrderItemsProductId",
                table: "Carts",
                columns: new[] { "OrderItemsOrderId", "OrderItemsProductId" },
                principalTable: "OrderItems",
                principalColumns: new[] { "OrderId", "ProductId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
