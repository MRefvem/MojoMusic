using Ecommerce_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models
{
    public class Instrument : Product
    {
        public string Manufacturer { get; set; }
        public string Type { get; set; }
    }
}
