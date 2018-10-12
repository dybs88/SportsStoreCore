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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SportsStore.DAL.Repos.DictionarySchema;

namespace SportsStore.Tests.Base
{
    public class MockedObjects
    {
        private static ISession _session;

        public static IServiceProvider Provider => GetProvider();

        public static IConfiguration Configuration => GetConfiguration();

        public static ISession Session => GetSession();

        public static IApplicationDbContext ApplicationDbContext => GetApplicationDbContext();

        public static ISportsStoreUserManager UserManager => GetUserManager();

        public static IDictionaryContainer DictionaryContainer => GetDictionaryContainer();

        private static IServiceProvider GetProvider()
        {
            Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();

            mockHttpContext.Setup(x => x.Session).Returns(GetSession());

            Mock<IHttpContextAccessor> mockContext = new Mock<IHttpContextAccessor>();
            mockContext.Setup(x => x.HttpContext).Returns(mockHttpContext.Object);

            Mock<IServiceProvider> mockProvider = new Mock<IServiceProvider>();
            mockProvider.Setup(x => x.GetService(It.IsAny<Type>())).Returns<Type>(t => 
            {
                if(t == typeof(IHttpContextAccessor))
                    return mockContext.Object;

                return null;
            });

            return mockProvider.Object;
        }

        private static IConfiguration GetConfiguration()
        {
            return new Mock<IConfiguration>().Object;
        }

        private static ISession GetSession()
        {
            if (_session == null)
                _session = new TestSession();

            return _session;
        }

        private static IApplicationDbContext GetApplicationDbContext()
        {
            Mock<IApplicationDbContext> mockContext = new Mock<IApplicationDbContext>();

            #region DbSet<Order>
            var sourceOrderList = Repositories.OrderRepository.Orders.ToList();
            Mock<DbSet<Order>> mockOrderSet = CreateMockedDbSet(sourceOrderList.AsQueryable());
            mockOrderSet.Setup(x => x.Add(It.IsAny<Order>())).Callback<Order>( o => 
            {
                sourceOrderList.Add(o);
            });
            mockOrderSet.Setup(x => x.Remove(It.IsAny<Order>())).Callback<Order>(o => 
            {
                sourceOrderList.Remove(o);
            });
            mockOrderSet.Setup(x => x.Find(It.IsAny<object[]>())).Returns<object[]>(input => 
            {
                return sourceOrderList.FirstOrDefault(o => o.OrderId == (int)input.First());
            });
            mockOrderSet.Setup(x => x.FindAsync(It.IsAny<object[]>())).Returns<object[]>(input => 
            {
                return Task.FromResult(sourceOrderList.FirstOrDefault(o => o.OrderId == (int)input.First()));
            });

            mockContext.Setup(x => x.Orders).Returns(mockOrderSet.Object);
            #endregion

            #region DbSet<Product>
            var sourceProductList = Repositories.ProductRepository.Products.ToList();
            Mock<DbSet<Product>> mockProductSet = CreateMockedDbSet(sourceProductList.AsQueryable());
            mockProductSet.Setup(x => x.Add(It.IsAny<Product>())).Callback<Product>(p =>
            {
                sourceProductList.Add(p);
            });
            mockProductSet.Setup(x => x.Remove(It.IsAny<Product>())).Callback<Product>(p =>
            {
                sourceProductList.Remove(p);
            });

            mockContext.Setup(x => x.Products).Returns(mockProductSet.Object);
            #endregion

            #region DbSet<Customer>
            var sourceCustomerList = Repositories.CustomerRepository.Customers.ToList();
            Mock<DbSet<Customer>> mockCustomerSet = CreateMockedDbSet(sourceCustomerList.AsQueryable());
            mockCustomerSet.Setup(x => x.Add(It.IsAny<Customer>())).Callback<Customer>(c =>
            {
                sourceCustomerList.Add(c);
            });
            mockCustomerSet.Setup(x => x.Remove(It.IsAny<Customer>())).Callback<Customer>(c => 
            {
                sourceCustomerList.Remove(c);
            });
            mockCustomerSet.Setup(x => x.Find(It.IsAny<object[]>())).Returns<object[]>(input => 
            {
                return sourceCustomerList.FirstOrDefault(c => c.CustomerId == (int)input.First());
            });

            mockContext.Setup(x => x.Customers).Returns(mockCustomerSet.Object);
            #endregion

            #region DbSet<Address>
            var sourceAddressList = Repositories.AddressRepository.Addresses.ToList();
            Mock<DbSet<Address>> mockAddressSet = CreateMockedDbSet(sourceAddressList.AsQueryable());
            mockAddressSet.Setup(x => x.Add(It.IsAny<Address>())).Callback<Address>(a =>
            {
                sourceAddressList.Add(a);
            });
            mockAddressSet.Setup(x => x.Remove(It.IsAny<Address>())).Callback<Address>(a => 
            {
                sourceAddressList.Remove(a);
            });

            mockContext.Setup(x => x.Addresses).Returns(mockAddressSet.Object);
            #endregion

            mockContext.Setup(x => x.Add(It.IsAny<Order>())).Callback<Order>((o) => 
            {
                mockOrderSet.Object.Add(o);
            });
            mockContext.Setup(x => x.Add(It.IsAny<Address>())).Callback<Address>((a) =>
            {
                mockAddressSet.Object.Add(a);
            });
            mockContext.Setup(x => x.Add(It.IsAny<Customer>())).Callback<Customer>((c) =>
            {
                mockCustomerSet.Object.Add(c);
            });
            mockContext.Setup(x => x.Add(It.IsAny<Product>())).Callback<Product>((p) =>
            {
                mockProductSet.Object.Add(p);
            });

            mockContext.Setup(x => x.SaveChanges()).Returns(() => 
            {
                if(sourceOrderList.Any(o => o.OrderId == 0))
                {
                    foreach(var newOrder in sourceOrderList.Where(o => o.OrderId == 0))
                    {
                        newOrder.OrderId = sourceOrderList.OrderByDescending(o => o.OrderId).First().OrderId + 1;
                    }
                }
                if(sourceAddressList.Any(a => a.AddressId == 0))
                {
                    foreach(var newAddress in sourceAddressList.Where(a => a.AddressId == 0))
                    {
                        newAddress.AddressId = sourceAddressList.OrderByDescending(a => a.AddressId).First().AddressId + 1;
                    }
                }

                if(sourceCustomerList.Any(c => c.CustomerId == 0))
                {
                    foreach(var newCustomer in sourceCustomerList.Where(c => c.CustomerId == 0))
                    {
                        newCustomer.CustomerId = sourceCustomerList.OrderByDescending(c => c.CustomerId).First().CustomerId + 1;
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

        private static IDictionaryContainer GetDictionaryContainer()
        {
            Mock<IDictionaryContainer> mockContainer = new Mock<IDictionaryContainer>();

            return mockContainer.Object;
        }

        private static Mock<DbSet<TEntity>> CreateMockedDbSet<TEntity>(IQueryable<TEntity> repository) where TEntity : class
        {
            Mock<DbSet<TEntity>> mockedDbSet = new Mock<DbSet<TEntity>>();
            mockedDbSet.As<IQueryable<TEntity>>().Setup(x => x.Provider).Returns(repository.Provider);
            mockedDbSet.As<IQueryable<TEntity>>().Setup(x => x.Expression).Returns(repository.Expression);
            mockedDbSet.As<IQueryable<TEntity>>().Setup(x => x.ElementType).Returns(repository.ElementType);
            mockedDbSet.As<IQueryable<TEntity>>().Setup(x => x.GetEnumerator()).Returns(() => { return repository.GetEnumerator(); });

            return mockedDbSet;
        }
    }
}
