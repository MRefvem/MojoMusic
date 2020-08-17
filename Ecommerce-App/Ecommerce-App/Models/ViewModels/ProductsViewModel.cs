using Ecommerce_App.Models;
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
        public List<Product> Prod { get; set; }
        public List<Instrument> Instruments { get; set; }
        public string Term { get; set; }
        public string Order { get; set; }
    }
}
