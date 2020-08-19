using Ecommerce_App.Data;
using Ecommerce_App.Models;
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
        private StoreDbContext _context;
        public ShoppingCartViewComponent(StoreDbContext context)
        {
            _context = context;
        }

        // make actions
        public async Task<IViewComponentResult> InvokeAsync(int number)
        {
            // do some logic to pull from the DB 
            var cartItems = await _context.CartItems.OrderByDescending(x => x.Id).Take(number).ToListAsync();

            return View(cartItems);
        }
    }
}
