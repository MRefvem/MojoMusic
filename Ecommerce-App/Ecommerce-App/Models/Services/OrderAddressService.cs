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
        /// Create - Method used to create the user's order address. User inputs their billing/shipping address into the form and that information is passed through the Checkout PageModel and through to the Order Service and then on to here.
        /// </summary>
        /// <param name="orderAddress">The input fields passed through from the user</param>
        /// <returns>The User's Order Address stored in the database</returns>
        public async Task<OrderAddress> Create(OrderAddress orderAddress)
        {
            _context.Entry(orderAddress).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return orderAddress;
        }
    }
}
