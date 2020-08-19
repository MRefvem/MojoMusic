using System;
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
        private IProduct _product;
        private IPayment _payment;

        public HomeController( IProduct product, IPayment payment )
        {
            _product = product;
            _payment = payment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var prods = await _product.GetProducts() ;

            ProductsViewModel vm = new ProductsViewModel
            {
                Prod = prods,
            };

            _payment.Run();

            return View(vm);
        }
    }
}
