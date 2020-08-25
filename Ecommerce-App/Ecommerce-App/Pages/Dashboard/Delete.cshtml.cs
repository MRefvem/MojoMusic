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
    public class DeleteModel : PageModel
    {
        private readonly IProduct _product;

        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public Product Product { get; set; }

        public DeleteModel(IProduct product)
        {
            _product = product;
        }

        public void OnGet()
        {

        }

        /// <summary>
        /// OnPost - When the administrator posts to this page, a query is sent to the product's database in the form of a .Delete method call using the Id number of the product chosen to be deleted from the system.
        /// </summary>
        /// <param name="Id">The Id number of the product chosen to be deleted from the product's database.</param>
        /// <returns>The completed task, the product associated with the inputted Id is now deleted from the database. User is redirected back to the admin portal.</returns>
        public async Task<IActionResult> OnPost(int Id)
        {
            Product.Id = Id;

            await _product.Delete(Id);

            return RedirectToPage("/Dashboard/AdminPortal");
        }
    }
}