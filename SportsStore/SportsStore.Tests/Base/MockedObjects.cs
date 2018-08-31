using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Moq;
using SportsStore.DAL.AbstractContexts;
using SportsStore.DAL.Repos.Security;
using SportsStore.Models.CustomerModels;
using SportsStore.Models.Identity;
using SportsStore.Models.OrderModels;
using SportsStore.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Tests.Base
{
    public class MockedObjects
    {
        public static IServiceProvider Provider => GetProvider();

        public static ISession Session => GetSession();

        public static IApplicationDbContext ApplicationDbContext => GetApplicationDbContext();

        public static ISportsStoreUserManager UserManager => GetUserManager();

        private static IServiceProvider GetProvider()
        {
            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();

            mockHttpContext.Setup(x => x.Session).Returns(GetSession());

            Mock<IHttpContextAccessor> mockContext = new Mock<IHttpContextAccessor>();
            mockContext.Setup(x => x.HttpContext).Returns(mockHttpContext.Object);

            Mock<IServiceProvider> mockProvider = new Mock<IServiceProvider>();
            mockProvider.Setup(x => x.GetService(It.IsAny<Type>())).Returns<Type>(t => 
            {
                if(t is IHttpContextAccessor)
                    return mockContext.Object;

                return null;
            });

            return mockProvider.Object;
        }

        private static ISession GetSession()
        {
            return new TestSession();
        }

        private static IApplicationDbContext GetApplicationDbContext()
        {
            Mock<IApplicationDbContext> mockContext = new Mock<IApplicationDbContext>();

            #region DbSet<Order>
            var queryableOrders = Repositories.OrderRepository.Orders.AsQueryable();
            Mock<DbSet<Order>> mockOrderSet = CreateMockedDbSet(queryableOrders);
            mockOrderSet.Setup(x => x.Add(It.IsAny<Order>())).Callback<Order>( o => 
            {
                queryableOrders = queryableOrders.Concat(new List<Order> { o });
            });
            mockOrderSet.Setup(x => x.Remove(It.IsAny<Order>())).Callback<Order>(o => 
            {
                queryableOrders = queryableOrders.Where(orders => orders.OrderId != o.OrderId);
            });
            mockOrderSet.Setup(x => x.Find(It.IsAny<object[]>())).Returns<object[]>(input => 
            {
                return queryableOrders.FirstOrDefault(o => o.OrderId == (int)input.First());
            });

            mockContext.Setup(x => x.Orders).Returns(mockOrderSet.Object);
            #endregion

            #region DbSet<Product>
            var queryableProducts = Repositories.ProductRepository.Products.AsQueryable();
            Mock<DbSet<Product>> mockProductSet = CreateMockedDbSet(queryableProducts);
            mockProductSet.Setup(x => x.Add(It.IsAny<Product>())).Callback<Product>(p =>
            {
                queryableProducts = queryableProducts.Concat(new List<Product> { p });
            });
            mockProductSet.Setup(x => x.Remove(It.IsAny<Product>())).Callback<Product>(p =>
            {
                queryableProducts = queryableProducts.Where(product => product.ProductID != p.ProductID);
            });

            mockContext.Setup(x => x.Products).Returns(mockProductSet.Object);
            #endregion

            #region DbSet<Customer>
            var queryableCustomers = Repositories.CustomerRepository.Customers.AsQueryable();
            Mock<DbSet<Customer>> mockCustomerSet = CreateMockedDbSet(queryableCustomers);
            mockCustomerSet.Setup(x => x.Add(It.IsAny<Customer>())).Callback<Customer>(c =>
            {
                queryableCustomers = queryableCustomers.Concat(new List<Customer> { c });
            });
            mockCustomerSet.Setup(x => x.Remove(It.IsAny<Customer>())).Callback<Customer>(c => 
            {
                queryableCustomers = queryableCustomers.Where(customer => customer.CustomerId != c.CustomerId);
            });
            mockCustomerSet.Setup(x => x.Find(It.IsAny<object[]>())).Returns<object[]>(input => 
            {
                return queryableCustomers.FirstOrDefault(c => c.CustomerId == (int)input.First());
            });

            mockContext.Setup(x => x.Customers).Returns(mockCustomerSet.Object);
            #endregion

            #region DbSet<Address>
            var queryableAddresses = Repositories.AddressRepository.Addresses.AsQueryable();
            Mock<DbSet<Address>> mockAddressSet = CreateMockedDbSet(queryableAddresses);
            mockAddressSet.Setup(x => x.Add(It.IsAny<Address>())).Callback<Address>(a =>
            {
                queryableAddresses = queryableAddresses.Concat(new List<Address> { a });
            });
            mockAddressSet.Setup(x => x.Remove(It.IsAny<Address>())).Callback<Address>(a => 
            {
                queryableAddresses = queryableAddresses.Where(address => address.AddressId != a.AddressId);
            });

            mockContext.Setup(x => x.Addresses).Returns(mockAddressSet.Object);
            #endregion

            mockContext.Setup(x => x.Add(It.IsAny<Order>())).Returns<Order>((o) => 
            {
                queryableOrders = queryableOrders.Concat(new List<Order> { o });
                return null;
            });
            mockContext.Setup(x => x.Add(It.IsAny<Address>())).Returns<Address>((a) =>
            {
                queryableAddresses = queryableAddresses.Concat(new List<Address> { a });
                return null;
            });
            mockContext.Setup(x => x.Add(It.IsAny<Customer>())).Returns<Customer>((c) =>
            {
                queryableCustomers = queryableCustomers.Concat(new List<Customer> { c });
                return null;
            });
            mockContext.Setup(x => x.Add(It.IsAny<Product>())).Returns<Product>((p) =>
            {
                queryableProducts = queryableProducts.Concat(new List<Product> { p });
                return null;
            });

            mockContext.Setup(x => x.SaveChanges()).Returns(() => 
            {
                if(queryableOrders.Any(o => o.OrderId == 0))
                {
                    foreach(var newOrder in queryableOrders.Where(o => o.OrderId == 0))
                    {
                        newOrder.OrderId = queryableOrders.OrderByDescending(o => o.OrderId).First().OrderId + 1;
                    }
                }
                if(queryableAddresses.Any(a => a.AddressId == 0))
                {
                    foreach(var newAddress in queryableAddresses.Where(a => a.AddressId == 0))
                    {
                        newAddress.AddressId = queryableAddresses.OrderByDescending(a => a.AddressId).First().AddressId + 1;
                    }
                }

                if(queryableCustomers.Any(c => c.CustomerId == 0))
                {
                    foreach(var newCustomer in queryableCustomers.Where(c => c.CustomerId == 0))
                    {
                        newCustomer.CustomerId = queryableCustomers.OrderByDescending(c => c.CustomerId).First().CustomerId + 1;
                    }
                }

                return 1;
            });

            return mockContext.Object;
        }

        private static ISportsStoreUserManager GetUserManager()
        {
            Mock<ISportsStoreUserManager> mockedUserManager = new Mock<ISportsStoreUserManager>();

            var users = Repositories.Users;
            mockedUserManager.Setup(x => x.Users).Returns(users);
            mockedUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).Returns<string>(email => 
            {
                var user =  users.FirstOrDefault(u => u.Email == email);
                return Task.FromResult(user);
            });


            return mockedUserManager.Object;
        }

        private static Mock<DbSet<TEntity>> CreateMockedDbSet<TEntity>(IQueryable<TEntity> repository) where TEntity : class
        {
            Mock<DbSet<TEntity>> mockedDbSet = new Mock<DbSet<TEntity>>();
            mockedDbSet.As<IQueryable<TEntity>>().Setup(x => x.Provider).Returns(repository.Provider);
            mockedDbSet.As<IQueryable<TEntity>>().Setup(x => x.Expression).Returns(repository.Expression);
            mockedDbSet.As<IQueryable<TEntity>>().Setup(x => x.ElementType).Returns(repository.ElementType);
            mockedDbSet.As<IQueryable<TEntity>>().Setup(x => x.GetEnumerator()).Returns(repository.GetEnumerator());

            return mockedDbSet;
        }
    }
}
