﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Product
    {
        public string Name { get; set; }
        public char Mfr { get; set; }
        public char Type { get; set; }
        public int Calories { get; set; }
        public int Protein { get; set; }
        public int Fat { get; set; }
        public int Sodium { get; set; }
        public decimal Fiber { get; set; }
        public decimal Carbo { get; set; }
        public int Sugars { get; set; }
        public int Potass { get; set; }
        public int Vitamins { get; set; }
        public int Shelf { get; set; }
        public decimal Weight { get; set; }
        public decimal Cups { get; set; }
        public decimal Rating { get; set; }

        public Product()
        {

        }
    }
}
