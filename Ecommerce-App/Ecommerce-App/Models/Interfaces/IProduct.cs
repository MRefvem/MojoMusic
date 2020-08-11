using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Interfaces
{
    public interface IProduct
    { 
        /// <summary>
        /// GetProducts - This method allows us to get a list of all of our products
        /// </summary>
        /// <returns>A list of all products</returns>
        List<Product> GetProducts();

        /// <summary>
        /// GetProduct - This method allows us to get a list of all products that match the searched name
        /// </summary>
        /// <param name="name">Takes in a string that represents the name searched on the front end</param>
        /// <returns>Returns a list containing all the objects that match the searched name</returns>
        List<Product> GetProduct(string name);

        /// <summary>
        /// Sort - This method allows us to sort the list of products alphabetically and reverse-alphabetically
        /// </summary>
        /// <param name="alphabetical">Takes in a string that correlates to button (alphabetical or reverse-alphabetical) selection on the front end</param>
        /// <returns>Returns a list of products according to selected button</returns>
        List<Product> Sort(string alphabetical);
    }
}
