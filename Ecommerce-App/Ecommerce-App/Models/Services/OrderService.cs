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
        /// Creates a new order
        /// </summary>
        /// <param name="UserEmail">the email of the user </param>
        /// <returns> a cart object</returns>
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
