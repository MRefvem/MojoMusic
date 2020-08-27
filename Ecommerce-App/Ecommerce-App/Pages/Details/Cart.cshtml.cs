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

        /// <summary>
        /// OnGet - This method performs the logic necessary to render all of the user's shopping cart information (with contents included) to the front end upon page load.
        /// </summary>
        /// <returns>The task complete, user's complete shopping cart data is available to be used on the front end. If for some reason a user made it this far without being assigned a shopping cart, one is instantiated in this method.</returns>
        public async Task<IActionResult> OnGet()
        {
            Cart cart = await _cart.GetCartForUserByEmail(GetUserEmail());

            if (cart == null)
            {
                await _cart.Create(GetUserEmail());
            }

            decimal totalPrice = 0;

            if (cart != null)
            {
                foreach (var item in cart.CartItems)
                {
                    totalPrice += item.Product.Price * item.Quantity;
                }
                cart.Total = totalPrice;
                await _cart.Update(cart);
                Total = totalPrice;

                CurrentCartId = cart.Id;
                CurrentUserCart = cart;
            }

            return Page();
        }

        /// <summary>
        /// OnPost - This method performs all of the logic necessary for when a user posts to this page. A user has the ability to update the quantity of individual items in their cart when they are on this page.
        /// </summary>
        /// <returns>The task complete, the quantity of the selected item in the cart has been modified (note: this is currently set to anywhere between 1 and 5 items).</returns>
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

        /// <summary>
        /// This method performs the logic necessary to successfully return the universal user's email address based on the customer's claims assigned to them upon registration.
        /// </summary>
        /// <returns>Returns a string containing that user's email address.</returns>
        protected string GetUserEmail()
        {
            return User.Claims.First(x => x.Type == "Email").Value;
        }
    }
}