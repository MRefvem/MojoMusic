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
    public class CartModel : PageModel
    {
        private readonly IProduct _product;
        private readonly ICartItems _cartItems;
        private readonly ICart _cart;

        [BindProperty]
        public Product Product { get; set; }
        [BindProperty]
        public int CurrentCartId { get; set; }

        public CartModel(IProduct product, ICartItems cartItems, ICart cart)
        {
            _product = product;
            _cartItems = cartItems;
            _cart = cart;
            CurrentCartId = 0;
        }

        public async Task<IActionResult> OnGet(int Id)
        {
            var prod = await _product.GetProduct(Id);
            Cart cart = await _cart.GetCartForUserByEmail(GetUserEmail());

            // logic here
            if (cart == null)
            {
                await _cart.Create(GetUserEmail());
            }

            var items = await _cartItems.GetAllCartItems(cart.Id);

            Product = prod;
            CurrentCartId = cart.Id;

            return Page();
        }

        protected string GetUserEmail()
        {
            return User.Claims.First(x => x.Type == "Email").Value;
        }
    }
}