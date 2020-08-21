﻿using Ecommerce_App.Data;
using Ecommerce_App.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Services
{
    public class CartService : ICart
    {
        private StoreDbContext _context;

        //brings in the storedb
        public CartService(StoreDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new cart
        /// </summary>
        /// <param name="UserEmail">the email of the user </param>
        /// <returns> a cart object</returns>
        public async Task<Cart> Create (string UserEmail)
        {
            Cart cart = new Cart()
            {
                UserEmail = UserEmail
            };
            _context.Entry(cart).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return cart;
        }

        /// <summary>
        /// Gets a cart by using the users email
        /// </summary>
        /// <param name="userEmail">the email of the user</param>
        /// <returns>  cart of the user  </returns>
        public async Task<Cart> GetCartForUserByEmail(string userEmail)
        {
           Cart cart = await _context.Carts.Where(x => x.UserEmail == userEmail)
                                            .Include(x => x.CartItems)
                                            .ThenInclude(x => x.Product)
                                            .FirstOrDefaultAsync();

            return cart;
        }

        
    }
}
