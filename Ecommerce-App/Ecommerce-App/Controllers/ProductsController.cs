using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;
using Ecommerce_App.Models.Interfaces;
using Ecommerce_App.Models.Services;
using Ecommerce_App.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Ecommerce.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProduct _product;

        public ProductsController(IProduct product)
        {
            _product = product;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Cereal> myList = _product.GetProducts().Cast<Cereal>().ToList();

            ProductsViewModel vm = new ProductsViewModel
            {
                Products = myList,
                Term = ""
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult SearchResults(string searchString)
        {
            // Force cast the data type
            List<Cereal> product = _product.GetProduct(searchString).Cast<Cereal>().ToList();

            ProductsViewModel vm = new ProductsViewModel
            {
                Products = product,
                Term = searchString
            };

            return View(vm);
        }

        public IActionResult Sort(string sortedString)
        {
            List<Cereal> products = _product.Sort(sortedString).Cast<Cereal>().ToList();

            ProductsViewModel vm = new ProductsViewModel
            {
                Products = products,
                Term = sortedString
            };

            return View(vm);
        }
    }
}
