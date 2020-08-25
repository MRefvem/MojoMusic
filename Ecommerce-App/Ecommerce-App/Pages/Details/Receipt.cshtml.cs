using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_App.Data;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_App.Pages.Details
{
    
    public class ReceiptModel : PageModel
    {
        private readonly UserManager<Customer> _userManager;
        private readonly StoreDbContext _context;

        public decimal Total { get; set; }
        public Cart CurrentUserCart { get; set; }
        public Order UserOrder { get; set; }

        public ReceiptModel(UserManager<Customer> userManager , StoreDbContext context )
        {
            _userManager = userManager;
            _context = context;
        }

        /// <summary>
        /// OnGet - This information gathers all of the necessary information from the database that is necessary to display upon loading the page. In this case, it is the details about the user's most-recent purchase.
        /// </summary>
        /// <returns>The task complete, the necessary information has been queried from the database and is now ready to present to the front end.</returns>
        public async Task<IActionResult> OnGet()
        {
                Customer user = await _userManager.GetUserAsync(User);

                var cart = await _context.Carts.Where(x => x.IsActive == false && x.UserEmail == user.Email)
                                              .Include(x => x.CartItems)
                                              .ThenInclude(x => x.Product)
                                              .OrderByDescending(x => x.Id)
                                              .FirstOrDefaultAsync();

              var order =  await _context.Order.Where(x => x.CartId == cart.Id)
                                                .Include(x => x.OrderAddress)
                                                .FirstOrDefaultAsync();

                CurrentUserCart = cart;
                UserOrder = order;

                decimal totalPrice = 0;

                foreach (var item in cart.CartItems)
                {
                    totalPrice += item.Product.Price * item.Quantity;
                }

                Total = totalPrice;

                return Page();
        }
    }
}
