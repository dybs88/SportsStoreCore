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
        public int OrderId { get; set; }
        public ICollection<CartItem> Items  { get; set; }
        public Customer Customer { get; set; }
        public bool GiftWrap { get; set; }
        public bool Shipped { get; set; }
        public decimal Value => Items.Any() ? Items.Sum(i => i.Value) : 0; 
    }
}
