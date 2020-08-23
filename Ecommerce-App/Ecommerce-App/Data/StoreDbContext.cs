using Ecommerce_App.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Tells DB that CartItems has a combination composite key of CartID and ProductId
            modelBuilder.Entity<CartItems>().HasKey(x => new { x.CartId, x.ProductId });
            // Tells DB that Order has a combination composite key of OrderAddressID and CartId
            modelBuilder.Entity<Order>().HasKey(x => new { x.OrderAddressId, x.CartId });
          
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    SKU = "213nvnv33",
                    Name = "Yamaha C7 Grand Piano",
                    Price = 48000.00m,
                    Description = "Yamaha's finest concert grand delivers the performance and subtleties expected from the world's leading pianists.",
                    Image = "https://401ecommerce.blob.core.windows.net/productimages/Yamaha%20C7%20Grand%20Piano.jpg"

                },
                new Product
                {
                    Id = 2,
                    SKU = "33mmml23",
                    Name = "Gemeinhardt Performance Flute",
                    Price = 1749.00m,
                    Description = "Gemeinhardt's level of craftsmanship has provided excellent flutes for generations of budding professionals. Take advantage of their prestine quality and our great offer today!",
                    Image = "https://401ecommerce.blob.core.windows.net/productimages/Gemeinhardt%20Performance%20Flute.jpg"

                }
                , new Product
                {
                    Id = 3,
                    SKU = "113nvev33",
                    Name = "Fender Stratocaster",
                    Price = 1149.99m,
                    Description = "Fender has been a leading innovator in electric guitar building for nearly a century, providing favorite models for legendary musicians such as Jimi Hendrix and more.",
                    Image = "https://401ecommerce.blob.core.windows.net/productimages/Fender%20Stratocaster.png"

                },
                 new Product
                 {
                     Id = 4,
                     SKU = "200llvnv33",
                     Name = "SM7B Vocal Microphone",
                     Price = 399.00m,
                     Description = "Whether it's broadcast, podcast or recording, voices need to be handled with care. When purified and polished, every detail has more impact. That's why the SM7B was built, to capture smooth, warm vocals that connect the speaker to the listener.",
                     Image = "https://401ecommerce.blob.core.windows.net/productimages/SM7B%20Vocal%20Microphone.jpg"

                 },
                 new Product
                 {
                     Id = 5,
                     SKU = "213nvnv44",
                     Name = "Beats by Dr. Dre",
                     Price = 349.99m,
                     Description = "These premium noise cancelling headphones ensure the highest fidelity sound can be enjoyed while also blocking out the distractions of everyday noises.",
                     Image = "https://401ecommerce.blob.core.windows.net/productimages/Beats%20by%20Dr.%20Dre.jpg"

                 },
                  new Product
                  {
                      Id = 6,
                      SKU = "213oonv33",
                      Name = "Mackie CR3-X 3 inch Multimedia Monitors",
                      Price = 99.00m,
                      Description = "Mackie CR3-X Multimedia Monitors put studio-quality sound right on your desktop at a great value. Mackie have earned their reputation for quality studio monitoring over the years, and CR3-X Multimedia Monitors are just as suitable for multimedia production as they are for gaming and casual listening.",
                      Image = "https://401ecommerce.blob.core.windows.net/productimages/Mackie%20CR3-X%203%20inch%20Multimedia%20Monitors.jfif"

                  },
                   new Product
                   {
                       Id = 7,
                       SKU = "122pppp222",
                       Name = "Hohner Special 20 Harmonica",
                       Price = 49.00m,
                       Description = "The Honer Special has been a favorite of blues musicians for decades, accompanying some of the legendary recoridngs from the early days of jazz masters all the way to present-day alternative rock.",
                       Image = "https://401ecommerce.blob.core.windows.net/productimages/Hohner%20Special%2020%20Harmonica.jpg"

                   },
                    new Product
                    {
                        Id = 8,
                        SKU = "213dddd33",
                        Name = "Lyon and Healy Concert Harp",
                        Price = 17875.00m,
                        Description = "Lyon and Healy have set the standard for concert level harp production through their focus on excellence and preference among leading professionals in the industry. They can be heard in almost every major symphony orchestra.",
                        Image = "https://401ecommerce.blob.core.windows.net/productimages/Lyon%20and%20Healy%20Concert%20Harp.jfif"

                    },
                     new Product
                     {
                         Id = 9,
                         SKU = "199rrnv33",
                         Name = "Taylor 100 Series Guitar",
                         Price = 799.00m,
                         Description = "Taylor has been recognized over time for their ability to perfectly capture the backwoods/rustic appeal in Country music. The Taylor 100 Series makes the perfect choice for budding singers across many genres.",
                         Image = "https://401ecommerce.blob.core.windows.net/productimages/Taylor%20100%20Series%20Guitar.jpg"

                     },
                      new Product
                      {
                          Id = 10,
                          SKU = "213oonv00",
                          Name = "Yamaha Absolute Hybrid Maple 5-Piece Drum Set",
                          Price = 3999.00m,
                          Description = "When Yamaha set out designing the Absolute Hybrid Maple, they started with the most important aspect of a musical instrument, the sound.",
                          Image = "https://401ecommerce.blob.core.windows.net/productimages/Yamaha%20Absolute%20Hybrid%20Maple%205-Piece%20Drum%20Set.jpg"
                      }

                );

            modelBuilder.Entity<Cart>().HasData(
             new Cart
             {
                 Id = 1,
                 UserEmail = "testcart@gmail.com"
             }
              );

            modelBuilder.Entity<CartItems>().HasData(
                new CartItems
                {
                    CartId = 1,
                    ProductId = 1,
                    Quantity = 2,
                },
                   new CartItems
                   {
                       CartId = 1,
                       ProductId = 3,
                       Quantity = 1,
                   }

                 );
        }
        public DbSet<Product> Products { get; set; }
        public  DbSet<Cart> Carts { get; set; }
        public DbSet<CartItems> CartItems { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderAddress> OrderAddress { get; set; }
    }
}
