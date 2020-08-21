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
        private ICartItems _cartItems;

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

        public async Task<IActionResult> OnPost(int productId, int cartId)
        {
            CartItem.ProductId = productId;
            CartItem.CartId = cartId;

            await _cartItems.Delete(productId, cartId);
            return RedirectToPage("/Details/Cart");
        }
    }
}