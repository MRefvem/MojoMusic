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

        private readonly ICartItems _cartItems;
        private readonly ICart _cart;

        [BindProperty]
        public int CurrentCartId { get; set; }
        public Cart CurrentUserCart { get; set; }
        [BindProperty]
        public int CartId { get; set; }
        [BindProperty]
        public int ProductId { get; set; }
        [BindProperty]
        public int Quantity { get; set; }
        public decimal Total { get; set; }

        public CartModel(ICartItems cartItems, ICart cart)
        {
            _cartItems = cartItems;
            _cart = cart;
            CurrentCartId = 0;
        }

        public async Task<IActionResult> OnGet()
        {
            Cart cart = await _cart.GetCartForUserByEmail(GetUserEmail());

            // logic here
            if (cart == null)
            {
                await _cart.Create(GetUserEmail());
            }

            decimal totalPrice = 0;

            foreach (var item in cart.CartItems)
            {
                totalPrice += item.Product.Price * item.Quantity;
            }

            Total = totalPrice;

            CurrentCartId = cart.Id;
            CurrentUserCart = cart;

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            CartItems cartItem = new CartItems()
            {
                CartId = CartId,
                ProductId = ProductId,
                Quantity = Quantity,
            };

            await _cartItems.Update(cartItem);

            return await OnGet();
        }

        protected string GetUserEmail()
        {
            return User.Claims.First(x => x.Type == "Email").Value;
        }
    }
}