using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce_App.Pages.Details
{
    public class RemoveModel : PageModel
    {
        private readonly ICartItems _cartItems;

        [BindProperty]
        public int ProductId { get; set; }
        [BindProperty]
        public int CartId { get; set; }
        [BindProperty]
        public CartItems CartItem { get; set; }


        public RemoveModel(ICartItems cartItems)
        {
            _cartItems = cartItems;
        }

        public void OnGet()
        {

        }

        /// <summary>
        /// This method performs the functions necessary to remove an item from a user's shopping cart.
        /// </summary>
        /// <param name="productId">A product Id is sent in as a parameter.</param>
        /// <param name="cartId">A cart Id is sent in, that is associated with the user's "IsActive" cart.</param>
        /// <returns>The task complete, the user's shopping cart no longer contains the item that was selected to be removed.</returns>
        public async Task<IActionResult> OnPost(int productId, int cartId)
        {
            CartItem.ProductId = productId;
            CartItem.CartId = cartId;

            await _cartItems.Delete(productId, cartId);
            return RedirectToPage("/Details/Cart");
        }
    }
}