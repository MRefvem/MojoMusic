using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Interfaces
{
    public interface IProduct
    {
        Product Create(Product product);

        List<Product> GetProducts();

        List<Product> GetProduct(string name);

        List<Product> Sort(string alphabetical);
    }
}
