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
        private readonly SignInManager<Customer> _signInManager;
        private readonly ICart _cart;
        private readonly ICartItems _cartItems;
        private readonly IOrderAddress _orderAddress;
        private readonly IOrder _order;
        private readonly StoreDbContext _context;

        public decimal Total { get; set; }
        public Cart CurrentUserCart { get; set; }
        public Order UserOrder { get; set; }

        public ReceiptModel(UserManager<Customer> userManager , SignInManager<Customer> signInManager, ICart cart, ICartItems cartItems, IOrderAddress orderAddress, IOrder order, StoreDbContext context )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _cart = cart;
            _cartItems = cartItems;
            _orderAddress = orderAddress;
            _order = order;
            _context = context;

        }
        public async Task<IActionResult> OnGet()
        {
                Customer user = await _userManager.GetUserAsync(User);
            /*  Cart cart = await _cart.GetCartForUserByEmail(user.Email);*/
            /*  Order order = await _order.GetMostRecent(cart.Id);*/

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