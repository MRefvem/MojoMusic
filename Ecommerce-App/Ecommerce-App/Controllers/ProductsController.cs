using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Ecommerce_App.Models.Services;
using Ecommerce_App.Models.ViewModels;
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var prods = await _product.GetProducts();

            ProductsViewModel vm = new ProductsViewModel
            {
                Prod = prods,
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> SearchResults(string searchString)
        {
            var prods = await _product.SearchByProductName(searchString);

            ProductsViewModel vm = new ProductsViewModel
            {
                Prod = prods,
            };

            return View(vm);
        }
    }
}
