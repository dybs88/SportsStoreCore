using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.OrderModels
{
    public class ListOrderViewModel
    {
        public int CustomerId { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
