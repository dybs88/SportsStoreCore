using SportsStore.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.DAL.Repos
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }


        void SaveOrder(Order Order);
    }
}
