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
        private StoreDbContext _context;

        public InventoryManagement(StoreDbContext context)
        {
            _context = context;
        }



        /// <summary>
        /// Create - allow us to create a product
        /// </summary>
        /// <param name="product"> product we want to create</param>
        /// <returns> created product object </returns>
        public async Task<Product> Create(Product product)
        {
            _context.Entry(product).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return product;
        }

        /// <summary>
        /// Delete - allows us to delete a product
        /// </summary>
        /// <param name="id"> id of the item we want to delete</param>
        /// <returns> task completion</returns>
        public async  Task Delete(int id)
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
        /// GetProduct - This method allows us to get a list of all products that match the searched name
        /// </summary>
        /// <param name="name">Takes in a string that represents the name searched on the front end</param>
        /// <returns>Returns a list containing all the objects that match the searched name</returns>
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
        /// Gets product by name
        /// </summary>
        /// <param name="name">name of the product</param>
        /// <returns> product object</returns>
        public async Task<Product> GetProductByName(string name)
        {
            Product product = await _context.Products.Where(x => x.Name == name).FirstOrDefaultAsync();
            return product;
        }

        /// <summary>
        /// GetProducts - This method allows us to get a list of all of our products
        /// </summary>
        /// <returns>A list of all products</returns>
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

        /*  public List<Product> Sort(string alphabetical)
          {
              List<Product> allProducts = GetProducts();

              switch (alphabetical)
              {
                  case "alphabetical":
                      allProducts.Sort((x, y) => string.Compare(x.Name, y.Name));
                      break;
                  case "reverseAlphabetical":
                      allProducts.Sort((x, y) => string.Compare(x.Name, y.Name));
                      allProducts.Reverse();
                      break;
                  default:
                      break;
              }

              return allProducts;
          }*/


        /// <summary>
        /// Update- allows us to update a product
        /// </summary>
        /// <param name="product"> product object we want to update</param>
        /// <returns> task completion </returns>
        public async Task<Product> Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
