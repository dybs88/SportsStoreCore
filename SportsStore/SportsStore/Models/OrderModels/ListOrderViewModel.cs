using SportsStore.Helpers;
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

        public PageHelper PageModel { get; set; }

        public ListOrderViewModel(int customerId, IEnumerable<Order> orders, int currentPage, int itemsPerPage, int totalItems)
        {
            CustomerId = customerId;
            Orders = orders;
            PageModel = new PageHelper { ItemPerPage = itemsPerPage, CurrentPage = currentPage, TotalItems = totalItems };
        }
    }
}
