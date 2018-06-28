using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SportsStore.Models.Cart;

namespace SportsStore.Models.Order
{
    public class Order
    {
        public Order()
        {
            Address = new Address();
        }

        [BindNever]
        public int OrderId { get; set; }
        [BindNever]
        public ICollection<CartItem> Items  { get; set; }

        [Required(ErrorMessage = "Podaj imię i nazwisko")]
        public string Name { get; set; }

        public Address Address { get; set; }

        public bool GiftWrap { get; set; }
        [BindNever]
        public bool Shipped { get; set; }
    }
}
