using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce_App.Migrations
{
    public partial class reseedingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    SKU = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "Image", "Name", "Price", "SKU" },
                values: new object[,]
                {
                    { 1, "Yamaha's finest concert grand delivers the performance and subtleties expected from the world's leading pianists.", "https://401ecommerce.blob.core.windows.net/productimages/Yamaha%20C7%20Grand%20Piano.jpg", "Yamaha C7 Grand Piano", 48000.00m, "213nvnv33" },
                    { 2, "Gemeinhardt's level of craftsmanship has provided excellent flutes for generations of budding professionals. Take advantage of their prestine quality and our great offer today!", "https://401ecommerce.blob.core.windows.net/productimages/Gemeinhardt%20Performance%20Flute.jpg", "Gemeinhardt Performance Flute", 1749.00m, "33mmml23" },
                    { 3, "Fender has been a leading innovator in electric guitar building for nearly a century, providing favorite models for legendary musicians such as Jimi Hendrix and more.", "https://401ecommerce.blob.core.windows.net/productimages/Fender%20Stratocaster.png", "Fender Stratocaster", 1149.99m, "113nvev33" },
                    { 4, "Whether it's broadcast, podcast or recording, voices need to be handled with care. When purified and polished, every detail has more impact. That's why the SM7B was built, to capture smooth, warm vocals that connect the speaker to the listener.", "https://401ecommerce.blob.core.windows.net/productimages/SM7B%20Vocal%20Microphone.jpg", "SM7B Vocal Microphone", 399.00m, "200llvnv33" },
                    { 5, "These premium noise cancelling headphones ensure the highest fidelity sound can be enjoyed while also blocking out the distractions of everyday noises.", "https://401ecommerce.blob.core.windows.net/productimages/Beats%20by%20Dr.%20Dre.jpg", "Beats by Dr. Dre", 349.99m, "213nvnv44" },
                    { 6, "Mackie CR3-X Multimedia Monitors put studio-quality sound right on your desktop at a great value. Mackie have earned their reputation for quality studio monitoring over the years, and CR3-X Multimedia Monitors are just as suitable for multimedia production as they are for gaming and casual listening.", "https://401ecommerce.blob.core.windows.net/productimages/Mackie%20CR3-X%203%20inch%20Multimedia%20Monitors.jfif", "Mackie CR3-X 3 inch Multimedia Monitors", 99.00m, "213oonv33" },
                    { 7, "The Honer Special has been a favorite of blues musicians for decades, accompanying some of the legendary recoridngs from the early days of jazz masters all the way to present-day alternative rock.", "https://401ecommerce.blob.core.windows.net/productimages/Hohner%20Special%2020%20Harmonica.jpg", "Hohner Special 20 Harmonica", 49.00m, "122pppp222" },
                    { 8, "Lyon and Healy have set the standard for concert level harp production through their focus on excellence and preference among leading professionals in the industry. They can be heard in almost every major symphony orchestra.", "https://401ecommerce.blob.core.windows.net/productimages/Lyon%20and%20Healy%20Concert%20Harp.jfif", "Lyon and Healy Concert Harp", 17875.00m, "213dddd33" },
                    { 9, "Taylor has been recognized over time for their ability to perfectly capture the backwoods/rustic appeal in Country music. The Taylor 100 Series makes the perfect choice for budding singers across many genres.", "https://401ecommerce.blob.core.windows.net/productimages/Taylor%20100%20Series%20Guitar.jpg", "Taylor 100 Series Guitar", 799.00m, "199rrnv33" },
                    { 10, "When Yamaha set out designing the Absolute Hybrid Maple, they started with the most important aspect of a musical instrument, the sound.", "https://401ecommerce.blob.core.windows.net/productimages/Yamaha%20Absolute%20Hybrid%20Maple%205-Piece%20Drum%20Set.jpg", "Yamaha Absolute Hybrid Maple 5-Piece Drum Set", 3999.00m, "213oonv00" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
