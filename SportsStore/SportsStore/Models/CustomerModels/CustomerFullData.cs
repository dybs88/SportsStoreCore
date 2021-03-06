﻿using SportsStore.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.CustomerModels
{
    public class CustomerFullData
    {
        public Customer Customer { get; set; }
        public CustomerAdditionalData AdditionalData { get; set; }
        public SportUser User { get; set; }
        public IEnumerable<Address> Addresses { get; set; }
    }
}
