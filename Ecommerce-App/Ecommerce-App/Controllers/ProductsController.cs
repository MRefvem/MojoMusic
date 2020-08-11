using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;
using Ecommerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProduct _product;

        public ProductsController(IProduct product)
        {
            _product = product;
        }

        public IActionResult Index()
        {
            List<Product> myList = _product.GetProducts();

            return View(myList);
        }

        public IActionResult SearchResults(string searchString)
        {
            List<Product> product = _product.GetProduct(searchString);

            return View(product);
        }

        public IActionResult Sort(string sortedString)
        {
            List<Product> products = _product.Sort(sortedString);

            return View(products);
        }
    }
}
