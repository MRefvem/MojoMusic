using Ecommerce_App.Data;
using Ecommerce_App.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Services
{
    public class OrderService : IOrder
    {
        private readonly StoreDbContext _context;
        private readonly IOrderAddress _orderAddress;

        public OrderService(StoreDbContext context, IOrderAddress orderAddress)
        {
            _context = context;
            _orderAddress = orderAddress;
        }

        /// <summary>
        /// Create - Method that creates a new order.
        /// </summary>
        /// <param name="cart">The cart details that need to be processed with the order.</param>
        /// <param name="orderAddress">The address that the order will send the ordered products to.</param>
        /// <returns>The completed order, user's products are enroute!</returns>
        public async Task<Order> Create(Cart cart, OrderAddress orderAddress)
        {
            orderAddress = await _orderAddress.Create(orderAddress);

            Order order = new Order()
            {
                CartId = cart.Id,
                OrderAddressId = orderAddress.Id
            };

            _context.Entry(order).State = EntityState.Added;
            await _context.SaveChangesAsync();
            
            return order;
        }

        /// <summary>
        /// GetMostRecent - Method that gets the user's most recent order for the purpose of rendering the order details to the post-purchase summary page.
        /// </summary>
        /// <param name="cartId">The Id of the newly-inactive cart.</param>
        /// <returns>The order detail of the user's most recent purchase.</returns>
        public async Task<Order> GetMostRecent(int cartId)
        {
            Order order = await _context.Order.Where(x => x.CartId == cartId)
                                               .Include(x=> x.OrderAddress)
                                               .Include(x => x.Cart)
                                               .ThenInclude(x => x.CartItems)
                                               .ThenInclude(x => x.Product)
                                               .FirstOrDefaultAsync();

            return order;
        }
    }
}
