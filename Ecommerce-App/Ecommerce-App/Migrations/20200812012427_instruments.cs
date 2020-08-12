using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce_App.Migrations
{
    public partial class instruments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Name", "Price", "SKU" },
                values: new object[,]
                {
                    { 1, "Delivers the quality and refinement you've come to expect from Celviano, in a compact-yet-elegant cabinet that compliments any space ", "Piano", 1000.00m, "213nvnv33" },
                    { 2, "Beautiful melodies ", "Fluet", 20.00m, "33mmml23" },
                    { 3, "Has a lot a base and has been used by Jimi Hendrix ", "Guitar", 300.00m, "113nvev33" },
                    { 4, " Basic microphone that can be hooked up to almost anything ", "Microphone", 100.00m, "200llvnv33" },
                    { 5, "Headphones designed by Dr.Dre ", "Beats Headphones", 500.00m, "213nvnv44" },
                    { 6, "Studio speakers", "Speaker", 1000.00m, "213oonv33" },
                    { 7, "Plastic harmonica", "Harmonica", 5.00m, "122pppp222" },
                    { 8, "Gold and diamond harp with silk strings", "Harp", 10000.00m, "213dddd33" },
                    { 9, "Black and Gold and comes with speaker", "Electric Guitar", 750.00m, "199rrnv33" },
                    { 10, "Full drum set", "Drum", 490.00m, "213oonv00" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
