using SportsStore.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.Cart
{
    [Table("Items", Schema = "Sales")]
    public class CartItem
    {
        public int CartItemId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Value => Product.Price * Quantity;
    }
}
