using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce_App.Data;
using Ecommerce_App.Models;
using Ecommerce_App.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce_App.Pages.Account
{
    public class OrderHistoryModel : PageModel
    {
        private readonly IOrder _order;
        private readonly ICart _cart;
        private readonly ICartItems _cartItems;

        public List<Order> Order { get; set; }
        public Cart Cart { get; set; }

        public decimal Total { get; set; }
        public OrderHistoryModel(IOrder order , ICart cart, ICartItems cartItems)
        {
            _order = order;
            _cart = cart;
            _cartItems = cartItems;
        }
        public async Task<IActionResult> OnGet()
        {
            List<Order> order = await _order.GetAllOrders(GetUserEmail());
            Order = order;
            Cart cart = await _cart.GetCartForUserByEmail(GetUserEmail());
            decimal totalPrice = 0;

            foreach (var item in cart.CartItems)
            {
                totalPrice += item.Product.Price * item.Quantity;
            }
            cart.Total = totalPrice;
          
            Total = totalPrice;
            return Page();
        }

        protected string GetUserEmail()
        {
            return User.Claims.First(x => x.Type == "Email").Value;
        }
    }
}