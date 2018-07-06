using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.ViewModels
{
    public class CartViewModel
    {
        public Cart.Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}
