using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SportsStore.DAL.Contexts;
using SportsStore.Domain;
using SportsStore.Models.Cart;
using SportsStore.Models.CustomerModels;
using SportsStore.Models.DAL.Repos.SalesSchema;
using SportsStore.Models.OrderModels;
using SportsStore.Tests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SportsStore.Tests.RepositoriesTests
{
    public class OrderRepoTests
    {
        private IOrderRepository _target;
        TestSession _session;

        public OrderRepoTests()
        {
            _session = (TestSession)MockedObjects.Session;
            _target = new OrderRepository(MockedObjects.Provider, MockedObjects.Configuration, MockedObjects.ApplicationDbContext, Repositories.AddressRepository);
        }

        [Fact]
        public void CanGetOrders()
        {
            //act
           var orders = _target.Orders;

            //assert
            Assert.Equal(4, orders.Count());
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(1, 3)]
        [InlineData(1, 4)]
        public void CanGetCustomerOrder(int customerId, int orderId)
        {
            //arrange
            _session.Clear();
            _session.Add(SessionData.CustomerId, customerId);

            //act
            Order order = _target.GetOrder(orderId);

            //assert
            Assert.NotNull(order);
            Assert.Equal(orderId, order.OrderId);
            Assert.NotEqual(0, order.CustomerId);
            Assert.NotEmpty(order.Items);
            Assert.NotEqual(0, order.AddressId);
            Assert.NotEqual(0, order.NetValue);
        }

        [Theory]
        [InlineData(3, 1)]
        [InlineData(2, 4)]
        [InlineData(4, 4)]
        public void CannotGetAnotherCustomerOrder(int customerId, int orderId)
        {
            //arrange
            _session.Clear();
            _session.Add(SessionData.CustomerId, customerId);

            //act
            Order order = _target.GetOrder(orderId);

            //assert
            Assert.Null(order);
        }
        
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(4)]
        public void CanCreateNewOrder(int customerId)
        {
            //arrange

            //act
            Order result = _target.CreateNewOrder(customerId);

            //assert
            Assert.Equal(customerId, result.CustomerId);
            Assert.Equal("5", result.OrderNumber);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void CanGetCustomerOrders(int customerId)
        {
            //arrange

            //act
            IEnumerable<Order> customerOrders = _target.GetCustomerOrders(customerId);

            //assert
            Assert.True(customerOrders.All(o => o.CustomerId == customerId));
            Assert.NotNull(customerOrders);
            Assert.NotEmpty(customerOrders);
        }

        [Theory]
        [InlineData(1,1)]
        [InlineData(2,2)]
        [InlineData(1,3)]
        public void CanCheckIfCustomerIsOrderOwner(int customerId, int orderId)
        {
            //arrange

            //act
            bool result = _target.CheckIfCustomerIsOrderOwner(customerId, orderId);

            //assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(3, 4, 3, "P7")]
        [InlineData(2, 0, 2, "P2")]
        public void CanSaveOrder(int customerId, int addressId, int quantity, string productName)
        {
            //arrange
            _session.Clear();
            _session.Add(SessionData.CustomerId, customerId);
            Order order = _target.CreateNewOrder(customerId);
            order.AddressId = addressId;
            if (addressId != 0)
                order.Address = Repositories.AddressRepository.Addresses.FirstOrDefault(a => a.AddressId == order.AddressId);
            else
                order.Address = new Address { City = "Wrocław", Street = "Gwieździsta", BuildingNumber = "66", Region = "dolnośląskie", Country = "Polska", ZipCode = "51-400", CustomerId = customerId };

            order.Items = new List<CartItem>
            {
                new CartItem { OrderId = order.OrderId, Quantity = quantity, Product = Repositories.ProductRepository.Products.FirstOrDefault(p => p.Name == productName)}
            };
            decimal totalPrice = order.Items.Sum(i => i.NetValue);

            //act
            int orderId = _target.SaveOrder(order);

            //assert
            Assert.Equal(totalPrice, order.NetValue);
            Assert.NotEqual(0, order.OrderId);
            Assert.Equal(orderId, order.OrderId);
            Assert.False(string.IsNullOrEmpty(order.OrderNumber));
        }
    }
}
