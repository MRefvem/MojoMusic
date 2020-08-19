using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.Interfaces
{
    public interface ICart
    {
        Task<Cart> Create(string UserEmail);

        Task<Cart> GetCartForUserByEmail(string userEmail);
    }
}
