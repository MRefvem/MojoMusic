using Ecommerce.Models;
using Ecommerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Services
{
    public class ProductRepository : IProduct
    {
        
        public Product Create(Product product)
        {
            throw new NotImplementedException();
        }

        public List<Product> Sort(string alphabetical)
        {
            List<Product> allProducts = GetProducts();

            switch (alphabetical)
            {
                case "alphabetical":
                    allProducts.Sort((x, y) => string.Compare(x.Name, y.Name));
                    break;
                case "reverseAlphabetical":
                    allProducts.Sort((x, y) => string.Compare(x.Name, y.Name));
                    allProducts.Reverse();
                    break;
                default:
                    break;
            }

            return allProducts;
        }

        public List<Product> GetProduct(string searchString)
        {
            List<Product> allProducts = GetProducts();

            List<Product> product = allProducts.Where(x => x.Name == searchString)
                                        .ToList();

            return product;
        }

        public List<Product> GetProducts()
        {
            string path = Environment.CurrentDirectory;
            string newPath = Path.GetFullPath(Path.Combine(path, @"wwwroot\cereal.csv"));
            string[] allLines = System.IO.File.ReadAllLines(newPath);

            List<Product> products = new List<Product>();


            for (int i = 1; i < allLines.Length; i++)
            {
                string[] item = allLines[i].Split(",");
                var product = new Product()
                {
                    Name = item[0],
                    Mfr = item[1],
                    Type = item[2],
                    Calories = int.Parse(item[3]),
                    Protein = int.Parse(item[4]),
                    Fat = int.Parse(item[5]),
                    Sodium = int.Parse(item[6]),
                    Fiber = decimal.Parse(item[7]),
                    Carbo = decimal.Parse(item[8]),
                    Sugars = int.Parse(item[9]),
                    Potass = int.Parse(item[10]),
                    Vitamins = int.Parse(item[11]),
                    Shelf = int.Parse(item[12]),
                    Weight = decimal.Parse(item[13]),
                    Cups = decimal.Parse(item[14]),
                    Rating = decimal.Parse(item[15])
                };

                products.Add(product);

            }

            return products;
        }
    }
}
