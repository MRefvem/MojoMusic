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
        private ICartItems _cartItems;

        public ShoppingCartViewComponent(StoreDbContext context, ICartItems cartItems)
        {
            _cartItems = cartItems;

        }

        // make actions
        public async Task<IViewComponentResult> InvokeAsync(int currentCartId)
        {
            // do some logic to pull from the DB 
            var cartItems = await _cartItems.GetAllCartItems(currentCartId);

            return View(cartItems);
        }
    }
}
