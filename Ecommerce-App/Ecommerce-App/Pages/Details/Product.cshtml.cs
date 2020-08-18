using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Ecommerce_App.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce_App.Pages.Details
{
    public class ProductModel : PageModel
    {
        private readonly IProduct _product;

        [BindProperty]
        public Product Product { get; set; }

        public ProductModel(IProduct product)
        {
            _product = product;
        }

        public async Task<IActionResult> OnPost(int Id)
        {
            var prod = await _product.GetProduct(Id);

            Product = prod;

            return Page();
        }
    }
}
