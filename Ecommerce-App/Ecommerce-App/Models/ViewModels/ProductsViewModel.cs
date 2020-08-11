using Ecommerce_App.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models.ViewModels
{
    public class ProductsViewModel
    {
        public List<Cereal> Products { get; set; }
        public string Term { get; set; }
        public string Order { get; set; }
    }
}
