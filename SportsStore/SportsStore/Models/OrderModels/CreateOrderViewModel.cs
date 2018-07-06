using SportsStore.Models.CustomerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.OrderModels
{
    public class CreateOrderViewModel
    {
        public Customer Customer { get; set; }
        public Address Address { get; set; }
        public bool GiftWrap { get; set; }
    }
}
