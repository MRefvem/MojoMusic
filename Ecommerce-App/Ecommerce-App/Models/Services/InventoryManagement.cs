﻿using Ecommerce.Models;
using Ecommerce_App.Data;
using Ecommerce_App.Models.Interfaces;
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
        public List<Product> GetProduct(string name)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProducts()
        {
            throw new NotImplementedException();
        }

        public List<Product> Sort(string alphabetical)
        {
            throw new NotImplementedException();
        }
    }
}
