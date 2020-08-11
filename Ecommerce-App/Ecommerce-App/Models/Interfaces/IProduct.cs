using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Interfaces
{
    interface IProduct
    {
        Product Create(Product product);

        List<Product> GetProducts();

        Product GetProduct(string name);
    }
}
