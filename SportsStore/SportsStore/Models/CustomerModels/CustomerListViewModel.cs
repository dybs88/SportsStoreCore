using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.CustomerModels
{
    public class CustomerListViewModel
    {
        public Dictionary<Customer, CustomerAdditionalData> Customers { get; set; }

        public CustomerListViewModel()
        {
            Customers = new Dictionary<Customer, CustomerAdditionalData>();
        }
    }
}
