using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.CustomerModels
{
    public class CustomerAdditionalData
    {
        public int CustomerId { get; set; }
        public int CustomerOrdersCount { get; set; }

        public CustomerAdditionalData(int customerId)
        {
            CustomerId = customerId;
        }
    }
}
