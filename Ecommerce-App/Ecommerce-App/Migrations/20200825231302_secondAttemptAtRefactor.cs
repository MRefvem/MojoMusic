using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce_App.Migrations
{
    public partial class secondAttemptAtRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Carts_CartId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems");

            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "Order",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "Order",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Carts_CartId",
                table: "Order",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Carts_CartId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "Order");

            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "Order",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Carts_CartId",
                table: "Order",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
