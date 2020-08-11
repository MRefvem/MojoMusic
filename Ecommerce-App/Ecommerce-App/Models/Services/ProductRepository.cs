using Ecommerce.Models;
using Ecommerce_App.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Services
{
    public class ProductRepository : IProduct
    {
        //string path = Environment.CurrentDirectory;
        //string newPath = Path.GetFullPath(Path.Combine(path, @"wwwroot\cereal.csv"));
        //string[] myFile = File.ReadAllLines(newPath);


        public Product Create(Product product)
        {
            throw new NotImplementedException();
        }

        public Product GetProduct(string name)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProducts()
        {
            throw new NotImplementedException();
        }
    }
}
