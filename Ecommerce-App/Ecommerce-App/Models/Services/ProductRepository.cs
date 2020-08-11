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

        /// <summary>
        /// Sort - This method allows us to sort the list of products alphabetically and reverse-alphabetically
        /// </summary>
        /// <param name="alphabetical">Takes in a string that correlates to button (alphabetical or reverse-alphabetical) selection on the front end</param>
        /// <returns>Returns a list of products according to selected button</returns>
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

        /// <summary>
        /// GetProduct - This method allows us to get a list of all products that match the searched name
        /// </summary>
        /// <param name="name">Takes in a string that represents the name searched on the front end</param>
        /// <returns>Returns a list containing all the objects that match the searched name</returns>
        public List<Product> GetProduct(string searchString)
        {
            List<Product> allProducts = GetProducts();

            List<Product> product = allProducts.Where(x => x.Name.Contains(searchString))
                                        .ToList();

            return product;
        }

        /// <summary>
        /// GetProducts - This method allows us to get a list of all of our products
        /// </summary>
        /// <returns>A list of all products</returns>
        public List<Product> GetProducts()
        {
            string path = Environment.CurrentDirectory;
            string newPath = Path.GetFullPath(Path.Combine(path, @"wwwroot\cereal.csv"));
            string[] allLines = System.IO.File.ReadAllLines(newPath);

            List<Product> products = new List<Product>();


            for (int i = 1; i < allLines.Length; i++)
            {
                string[] item = allLines[i].Split(",");
                products.Add(new Cereal
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
                });

            }

            return products;
        }
    }
}
