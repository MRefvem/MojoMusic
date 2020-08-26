using Ecommerce_App.Data;
using Ecommerce_App.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Services
{
    public class OrderService : IOrder
    {
        private readonly StoreDbContext _context;
        private readonly ICartItems _cartItems;
        private readonly ICart _cart;

        public OrderService(StoreDbContext context, ICartItems cartItems, ICart cart)
        {
            _context = context;
            _cartItems = cartItems;
            _cart = cart;
        }

        /// <summary>
        /// Create - Method that creates a new order.
        /// </summary>
        /// <param name="cart">The cart details that need to be processed with the order.</param>
        /// <param name="orderAddress">The address that the order will send the ordered products to.</param>
        /// <returns>The completed order, user's products are enroute!</returns>
        public async Task<Order> Create(Order order)
        {
            _context.Entry(order).State = EntityState.Added;
            await _context.SaveChangesAsync();
            
            return order;
        }

        /// <summary>
        /// GetMostRecent - Method that gets the user's most recent order for the purpose of rendering the order details to the post-purchase summary page.
        /// </summary>
        /// <param name="cartId">The email (username) of the logged in user.</param>
        /// <returns>The order detail of the user's most recent purchase.</returns>
        public async Task<Order> GetMostRecent(string userEmail)
        {
            Order order = await _context.Order.Where(x => x.UserEmail == userEmail)
                                               .Include(x => x.Cart)
                                               .ThenInclude(x => x.CartItems)
                                               .ThenInclude(x => x.Product)
                                               .FirstOrDefaultAsync();

            return order;
        }
    }
}
