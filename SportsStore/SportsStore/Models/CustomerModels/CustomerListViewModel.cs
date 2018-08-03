using SportsStore.Helpers;
using SportsStore.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.CustomerModels
{
    public class CustomerListViewModel : BaseListViewModel
    {
        public Dictionary<Customer, CustomerAdditionalData> Customers { get; set; }

        public CustomerListViewModel(int currentPage, int itemsPerPage, int totalItems)
        {
            Customers = new Dictionary<Customer, CustomerAdditionalData>();
            PageModel = new PageHelper { ItemPerPage = itemsPerPage, CurrentPage = currentPage, TotalItems = totalItems };
        }
    }
}
