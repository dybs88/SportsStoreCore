using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportsStore.DAL.Contexts;
using SportsStore.Models.OrderModels;

namespace SportsStore.Models.DAL.Repos
{
    public class OrderRepository : IOrderRepository
    {
        private ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Order> Orders => _context.Orders
            .Include(o => o.Items)
            .ThenInclude(o => o.Product);

        public void SaveOrder(Order order)
        {
            _context.AttachRange(order.Items.Select(i => i.Product));
            if (order.OrderId == 0)
            {
                _context.Add(order);
            }

            _context.SaveChanges();
        }
    }
}
