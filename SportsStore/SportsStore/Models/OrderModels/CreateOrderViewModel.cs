using SportsStore.Models.CustomerModels;
using SportsStore.Models.DAL.Repos.SalesSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.OrderModels
{
    public class CreateOrderViewModel
    {
        public CustomerFullData CustomerFullData { get; set; }
        public Address SelectedAddress { get; set; }
        public bool GiftWrap { get; set; }
    }
}
