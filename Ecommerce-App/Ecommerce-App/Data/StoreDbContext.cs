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

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    SKU = "213nvnv33",
                    Name = "Piano",
                    Price = 1000.00m,
                    Description = "Delivers the quality and refinement you've come to expect from Celviano, in a compact-yet-elegant cabinet that compliments any space "

                },
                new Product
                {
                    Id = 2,
                    SKU = "33mmml23",
                    Name = "Fluet",
                    Price = 20.00m,
                    Description = "Beautiful melodies "

                }
                , new Product
                {
                    Id = 3,
                    SKU = "113nvev33",
                    Name = "Guitar",
                    Price = 300.00m,
                    Description = "Has a lot a base and has been used by Jimi Hendrix "

                },
                 new Product
                 {
                     Id = 4,
                     SKU = "200llvnv33",
                     Name = "Microphone",
                     Price = 100.00m,
                     Description = " Basic microphone that can be hooked up to almost anything "

                 },
                 new Product
                 {
                     Id = 5,
                     SKU = "213nvnv44",
                     Name = "Beats Headphones",
                     Price = 500.00m,
                     Description = "Headphones designed by Dr.Dre "

                 },
                  new Product
                  {
                      Id = 6,
                      SKU = "213oonv33",
                      Name = "Speaker",
                      Price = 1000.00m,
                      Description = "Studio speakers"

                  },
                   new Product
                   {
                       Id = 7,
                       SKU = "122pppp222",
                       Name = "Harmonica",
                       Price = 5.00m,
                       Description = "Plastic harmonica"

                   },
                    new Product
                    {
                        Id = 8,
                        SKU = "213dddd33",
                        Name = "Harp",
                        Price = 10000.00m,
                        Description = "Gold and diamond harp with silk strings"

                    },
                     new Product
                     {
                         Id = 9,
                         SKU = "199rrnv33",
                         Name = "Electric Guitar",
                         Price = 750.00m,
                         Description = "Black and Gold and comes with speaker"

                     },
                      new Product
                      {
                          Id = 10,
                          SKU = "213oonv00",
                          Name = "Drum",
                          Price = 490.00m,
                          Description = "Full drum set"
                      }

                );
        }
        public DbSet<Product> Products { get; set; }
    }
}
