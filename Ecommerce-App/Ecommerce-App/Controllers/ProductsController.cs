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

        /// <summary>
        /// Method controls what data is retrieved from the database and ready to display to the user upon page load.
        /// </summary>
        /// <returns>The task complete, information is gathered from the database and ready to display to the Razor View.</returns>
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

        /// <summary>
        /// SearchResults - Method that uses the SearchByProductName method to retrieve information about a specific product (or range of products) that was searched for by the user in the search bar.
        /// </summary>
        /// <param name="searchString">The unputted string used to query the database for more relevant info.</param>
        /// <returns>The task complete, searched-for product information delivered to the user through a query made to the database for the relevant information.</returns>
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
