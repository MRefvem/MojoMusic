﻿using Ecommerce_App.Data;
using Ecommerce_App.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Services
{
    public class CartItemsService : ICartItems
    {
        private StoreDbContext _context;
        private IProduct _product;
        private ICart _cart;

        public CartItemsService(StoreDbContext context, IProduct product, ICart cart)
        {
            _context = context;
            _product = product;
            _cart = cart;
        }

        public async Task<CartItems> Create(CartItems cartItems, int CartId)
        {
            cartItems.CartId = CartId;
            _context.Entry(cartItems).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return cartItems;
        }

        public async Task<CartItems> GetCartItem (int CartId,int ProductId)
        {
            CartItems cartItem = await _context.CartItems.Where(x => x.CartId == CartId && x.ProductId == ProductId)
                                                           .Include(x => x.Product)
                                                           .FirstOrDefaultAsync();

            return cartItem;
        }

        public async Task<List<CartItems>> GetAllCartItems(int CartId)
        {
            List<CartItems> cartItems = await _context.CartItems.Where(x => x.CartId == CartId)
                                                                .Include(x => x.Product)
                                                                .ToListAsync();
            return cartItems;
        }

        public async Task<CartItems> Update(CartItems cartItems)
        {

            _context.Entry(cartItems).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return cartItems;
        }

        public async Task Delete(int id)
        {
            CartItems cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null)
            {
                return;
            }
            else
            {
                _context.Entry(cartItem).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
        }
    }
}
