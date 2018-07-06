using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.CustomerModels
{
    public class CreateAddressViewModel
    {
        public int CustomerId { get; set; }
        public Address Address { get; set; }
    }
}
