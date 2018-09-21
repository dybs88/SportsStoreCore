using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SportsStore.DAL.AbstractContexts;
using SportsStore.DAL.Contexts;
using SportsStore.DAL.Repos;
using SportsStore.DAL.Repos.CustomerSchema;
using SportsStore.Domain;
using SportsStore.Infrastructure.Extensions;
using SportsStore.Models.CustomerModels;
using SportsStore.Models.Identity;
using SportsStore.Models.OrderModels;

namespace SportsStore.Models.DAL.Repos.SalesSchema
{
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        private IApplicationDbContext _context;
        private IAddressRepository _addressRepository;

        public OrderRepository(IServiceProvider provider, IConfiguration config, IApplicationDbContext context, IAddressRepository addressRepo)
            : base(provider, config)
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
            if(CheckIfCustomerIsOrderOwner(_session.GetJson<int>(SessionData.CustomerId),orderId))
                return Orders.FirstOrDefault(o => o.OrderId == orderId);

            return null;
        }

        public Order GetOrder(string orderNumber)
        {
            Order result = Orders.FirstOrDefault(o => o.OrderNumber == orderNumber);
            if (CheckIfCustomerIsOrderOwner(_session.GetJson<int>(SessionData.CustomerId), result.OrderId))
                return result;

            return null;
        }

        public IEnumerable<Order> GetCustomerOrders(int customerId)
        {
            if (customerId == 0)
                return Orders;

            return Orders.Where(o => o.CustomerId == customerId);
        }

        public int SaveOrder(Order order)
        {
            order.AddressId = _addressRepository.SaveAddress(order.Address);
            order.NetValue = order.Items.Sum(i => i.Value);
            if (order.OrderId == 0)
                _context.Add(order);

            _context.SaveChanges();

            return order.OrderId;
        }
    }
}
