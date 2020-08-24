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
    public class OrderAddressService : IOrderAddress
    {
        private readonly StoreDbContext _context;

        public OrderAddressService(StoreDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates an order address
        /// </summary>
        /// <param name="cartItems"> the cart item object</param>
        /// <returns> the created cartitem object</returns>
        public async Task<OrderAddress> Create(OrderAddress orderAddress)
        {
            _context.Entry(orderAddress).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return orderAddress;
        }

       
    }
}
