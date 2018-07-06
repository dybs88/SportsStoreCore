using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SportsStore.Models.Cart;
using SportsStore.Models.CustomerModels;

namespace SportsStore.Models.OrderModels
{
    [Table("SalesOrders", Schema = "Sales")]
    public class Order
    {
        [BindNever]
        public int OrderId { get; set; }
        [BindNever]
        public ICollection<CartItem> Items  { get; set; }

        [Required(ErrorMessage = "Podaj imię i nazwisko")]
        public string Name { get; set; }

        public Customer Customer { get; set; }

        public bool GiftWrap { get; set; }
        [BindNever]
        public bool Shipped { get; set; }
    }
}
