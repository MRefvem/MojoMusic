using Ecommerce_App.Data;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Components
{
    [ViewComponent]
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly ICartItems _cartItems;

        public ShoppingCartViewComponent(ICartItems cartItems)
        {
            _cartItems = cartItems;
        }

        /// <summary>
        /// InvokeAsync - Method that allows the program to pull information about the user's current shopping cart items from the database in order to display them to the View Component.
        /// </summary>
        /// <param name="currentCartId">The Id number of the user's current cart.</param>
        /// <returns>The task complete, the user's current cart information (with all items currently contained), returned to the user.</returns>
        public async Task<IViewComponentResult> InvokeAsync(int currentCartId)
        {
            var cartItems = await _cartItems.GetAllCartItems(currentCartId);

            return View(cartItems);
        }
    }
}
