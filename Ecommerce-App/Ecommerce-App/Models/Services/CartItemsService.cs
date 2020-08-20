using Ecommerce_App.Data;
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

        // brings in the storedb,product interface and cart interface
        public CartItemsService(StoreDbContext context, IProduct product, ICart cart)
        {
            _context = context;
            _product = product;
            _cart = cart;
        }

        /// <summary>
        /// Creates a cart item
        /// </summary>
        /// <param name="cartItems"> the cart item object</param>
        /// <returns> the created cartitem object</returns>
        public async Task<CartItems> Create(CartItems cartItems)
        {
            _context.Entry(cartItems).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return cartItems;
        }

        /// <summary>
        /// Gets a single cart item by the cartid and product id
        /// </summary>
        /// <param name="CartId"> the id of the userscart</param>
        /// <param name="ProductId"> the id of the product</param>
        /// <returns> the cart item object</returns>
        public async Task<CartItems> GetCartItem (int CartId,int ProductId)
        {
            CartItems cartItem = await _context.CartItems.Where(x => x.CartId == CartId && x.ProductId == ProductId)
                                                           .Include(x => x.Product)
                                                           .FirstOrDefaultAsync();

            return cartItem;
        }

        /// <summary>
        /// Gets all cart items by the cartid
        /// </summary>
        /// <param name="CartId">the users cart id</param>
        /// <returns>the cart item object</returns>
        public async Task<List<CartItems>> GetAllCartItems(int CartId)
        {
            List<CartItems> cartItems = await _context.CartItems.Where(x => x.CartId == CartId)
                                                                .Include(x => x.Product)
                                                                .ToListAsync();
            return cartItems;
        }

        /// <summary>
        /// Updates a cart item
        /// </summary>
        /// <param name="cartItems"> the cart item object</param>
        /// <returns> the updated cart</returns>
        public async Task<CartItems> Update(CartItems cartItems)
        {

            _context.Entry(cartItems).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return cartItems;
        }

        /// <summary>
        /// Deletes a cartitem
        /// </summary>
        /// <param name="id"> the id of the cart item</param>
        /// <returns> task completion</returns>
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
