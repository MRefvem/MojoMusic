using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce_App.Migrations
{
    public partial class firstAttemptAtNewOrderItemsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Carts_CartId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_OrderAddress_OrderAddressId",
                table: "Order");

            migrationBuilder.DropTable(
                name: "OrderAddress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_CartId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "OrderAddressId",
                table: "Order");

            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "Order",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Order",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Zip",
                table: "Order",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderItemsOrderId",
                table: "Carts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderItemsProductId",
                table: "Carts",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
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
                name: "IX_Order_CartId",
                table: "Order",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_OrderItemsOrderId_OrderItemsProductId",
                table: "Carts",
                columns: new[] { "OrderItemsOrderId", "OrderItemsProductId" });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId",
                unique: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Carts_CartId",
                table: "Order",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_OrderItems_OrderItemsOrderId_OrderItemsProductId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Carts_CartId",
                table: "Order");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_CartId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Carts_OrderItemsOrderId_OrderItemsProductId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Zip",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "OrderItemsOrderId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "OrderItemsProductId",
                table: "Carts");

            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "Order",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderAddressId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                columns: new[] { "OrderAddressId", "CartId" });

            migrationBuilder.CreateTable(
                name: "OrderAddress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Zip = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderAddress", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_CartId",
                table: "Order",
                column: "CartId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Carts_CartId",
                table: "Order",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_OrderAddress_OrderAddressId",
                table: "Order",
                column: "OrderAddressId",
                principalTable: "OrderAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
