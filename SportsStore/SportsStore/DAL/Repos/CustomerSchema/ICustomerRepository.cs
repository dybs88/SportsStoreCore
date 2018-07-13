﻿using SportsStore.Models.CustomerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.DAL.Repos.CustomerSchema
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> Customers { get; }

        void DeleteCustomer(int customerId);

        Customer GetCustomer(int customerId);

        Task<CustomerFullData> GetCustomerFullData(int customerId);

        int SaveCustomer(Customer customer);
    }
}
