﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_App.Models
{
    public class Cart
    {
        public int Id { get; set; }
        [EmailAddress]
        public string UserEmail { get; set; }
        public bool IsActive { get; set; }

        /// Navigation PROPERTIES
        public  List<CartItems> CartItems { get; set; } 
        public Order Orders { get; set; }
    }
}
