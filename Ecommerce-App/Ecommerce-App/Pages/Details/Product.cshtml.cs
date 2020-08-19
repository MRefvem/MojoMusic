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
        private readonly ICartItems _cartItems;
        private readonly ICart _cart;

        [BindProperty]
        public Product Product { get; set; }

        public ProductModel(IProduct product, ICartItems cartItems, ICart cart)
        {
            _product = product;
            _cartItems = cartItems;
            _cart = cart;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost(int Id)
        {
            var prod = await _product.GetProduct(Id);
            var cart = await _cart.GetCartForUserByEmail(GetUserEmail());

            CartItems cartItems = new CartItems
            {
                ProductId = Id,
                CartId = cart.Id,
                Quantity = 1
            };
            await _cartItems.Create(cartItems);

            Product = prod;

            return Page();
        }

        protected string GetUserEmail()
        {
            return User.Claims.First(x => x.Type == "Email").Value;
        }
    }

    
}
