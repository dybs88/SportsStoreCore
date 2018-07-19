﻿using SportsStore.Models.CustomerModels;
using SportsStore.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.DAL.Repos.SalesSchema
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }
        Order CreateNewOrder(int customerId);
        bool CheckIfCustomerIsOrderOwner(int customerId, int orderId);
        IEnumerable<Order> GetCustomerOrders(int customerId);
        Order GetOrder(int orderId);
        void SaveOrder(Order Order);
    }
}
