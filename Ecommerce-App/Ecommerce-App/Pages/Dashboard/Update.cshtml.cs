using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce_App.Pages.Dashboard
{
    [Authorize(Policy = "AdminOnly")]
    public class UpdateModel : PageModel
    {
        private readonly IProduct _product;

        [BindProperty]
        public Product Product { get; set; }

        public UpdateModel(IProduct product)
        {
            _product = product;
        }

        /// <summary>
        /// Upon page load, this method performs the logic necessary to get information about a specific product from the database and display that info onto the page.
        /// </summary>
        /// <param name="Id">The Id number associated with the specific product queried.</param>
        /// <returns>The completed task, the product now rendered from the database and to the page.</returns>
        public async Task<IActionResult> OnGet(int Id)
        {
            var prod = await _product.GetProduct(Id);

            Product = prod;

            return Page();
        }

        /// <summary>
        /// Upon posting to the page, this method performs the operations necessary to call the .Update method for the product entered and then make those changes in the database.
        /// </summary>
        /// <returns>The task complete, the product is now updated with all of the updated information.</returns>
        public async Task<IActionResult> OnPost()
        {
            await _product.Update(Product);

            return await OnGet(Product.Id);
        }
    }
}