﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Ecommerce_App.Models.Services;
using Ecommerce_App.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProduct _product;

        public HomeController(IProduct product)
        {
            _product = product;
        }

        /// <summary>
        /// Controls what information is available for the page to display upon page load.
        /// </summary>
        /// <returns>The task complete, information is gathered from the database and ready to display to the Razor View.</returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var prods = await _product.GetProducts() ;

            ProductsViewModel vm = new ProductsViewModel
            {
                Prod = prods,
            };

            return View(vm);
        }
    }
}
