using Ecommerce_App.Models;
using Ecommerce_App.Data;
using Ecommerce_App.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Services
{
    public class InventoryManagement : IProduct
    {
        private readonly StoreDbContext _context;

        public InventoryManagement(StoreDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create - Method that allow us to create a new product.
        /// </summary>
        /// <param name="product">The product we want to create.</param>
        /// <returns>Information about the newly created product.</returns>
        public async Task<Product> Create(Product product)
        {
            _context.Entry(product).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return product;
        }

        /// <summary>
        /// Delete - Method that allows the program to delete a product.
        /// </summary>
        /// <param name="id">The id of the item the administrator has chosen to delete.</param>
        /// <returns>The task completion, the stored product now deleted from the database.</returns>
        public async Task Delete(int id)
        {
            Product product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return;
            }
            else
            {
                _context.Entry(product).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }  
        }

        /// <summary>
        /// GetProduct - This method allows us to get a list of all products that match the searched name.
        /// </summary>
        /// <param name="name">Takes in a string that represents the name searched on the front end.</param>
        /// <returns>Returns a list containing all the objects that match the searched name.</returns>
        public async Task<Product> GetProduct(int Id)
        {
            var product = await _context.Products.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (product == null)
            {
                return null;
            }
            else
            {
                return product;
            }
        }

        /// <summary>
        /// GetProductByName - Gets product by name.
        /// </summary>
        /// <param name="name">The name of the product the program wants to find more information for.</param>
        /// <returns>The task complete, product information found.</returns>
        public async Task<Product> GetProductByName(string name)
        {
            Product product = await _context.Products.Where(x => x.Name == name).FirstOrDefaultAsync();
            return product;
        }

        /// <summary>
        /// GetProduct - This method allows us to get a list of all products that match the searched name.
        /// </summary>
        /// <param name="name">Takes in a string that represents the name searched on the front end.</param>
        /// <returns>Returns a list containing all the objects that match the searched name.</returns>
        public async Task<List<Product>> SearchByProductName(string searchString)
        {
            List<Product> allProducts = await GetProducts();

            List<Product> product = allProducts.Where(x => x.Name.Contains(searchString))
                                                                .ToList();

            return product;
        }

        /// <summary>
        /// GetProducts - This method allows us to get a list of all of our products.
        /// </summary>
        /// <returns>A list of all products in the database.</returns>
        public async Task<List<Product>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();

            List<Product> list = new List<Product>();

            foreach (var product in products)
            {
                list.Add(await GetProduct(product.Id));
            }
            return list;
        }

        /// <summary>
        /// Update - Method allows the program to update information about a product.
        /// </summary>
        /// <param name="product">The product the administrator seeks to update information about.</param>
        /// <returns>The task complete: the product is now updated with the inputted information.</returns>
        public async Task<Product> Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
