using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportsStore.DAL.Contexts;
using SportsStore.DAL.Repos.CustomerSchema;
using SportsStore.Models.CustomerModels;
using SportsStore.Models.OrderModels;

namespace SportsStore.Models.DAL.Repos.SalesSchema
{
    public class OrderRepository : IOrderRepository
    {
        private ApplicationDbContext _context;
        private IAddressRepository _addressRepository;

        public OrderRepository(ApplicationDbContext context, IAddressRepository addressRepo)
        {
            _context = context;
            _addressRepository = addressRepo;
        }

        public IEnumerable<Order> Orders => _context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Address)
            .Include(o => o.Items)
            .ThenInclude(i => i.Product);

        public Order CreateNewOrder(int customerId)
        {
            int newOrderNumber = 1;
            var lastOrder = Orders.OrderByDescending(o => int.Parse(o.OrderNumber)).FirstOrDefault();
            
            if(lastOrder != null)
            {
                newOrderNumber = int.Parse(lastOrder.OrderNumber) + 1;
            }

            Order newOrder = new Order(customerId);
            newOrder.OrderNumber = newOrderNumber.ToString();

            return newOrder;
        }

        public bool CheckIfCustomerIsOrderOwner(int customerId, int orderId)
        {
            return GetCustomerOrders(customerId)?.FirstOrDefault(o => o.OrderId == orderId) != null;
        }

        public Order GetOrder(int orderId)
        {
            return Orders.FirstOrDefault(o => o.OrderId == orderId);
        }

        public IEnumerable<Order> GetCustomerOrders(int customerId)
        {
            return Orders.Where(o => o.CustomerId == customerId);
        }

        public void SaveOrder(Order order)
        {
            order.AddressId = _addressRepository.SaveAddress(order.Address);

            if (order.OrderId == 0)
                _context.Add(order);

            _context.SaveChanges();
        }
    }
}
