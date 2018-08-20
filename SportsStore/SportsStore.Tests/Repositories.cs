using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Moq;
using SportsStore.DAL.Repos;
using SportsStore.DAL.Repos.CustomerSchema;
using SportsStore.Models.Cart;
using SportsStore.Models.CustomerModels;
using SportsStore.Models.DAL.Repos.SalesSchema;
using SportsStore.Models.Identity;
using SportsStore.Models.OrderModels;
using SportsStore.Models.ProductModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SportsStore.Tests
{
    public class Repositories
    {
        public static ICustomerRepository CustomerRepository => GetCustomerRepository();

        public static IAddressRepository AddressRepository => GetAddressRepository();

        public static IOrderRepository OrderRepository => GetOrderRepository();

        public static List<CartItem> Items => GetCartItems();

        public static IProductRepository ProductRepository => GetProductRepository();

        public static IQueryable<SportUser> Users => GetUsers();





        private static ICustomerRepository GetCustomerRepository()
        {
            Mock<ICustomerRepository> mockCustRepo = new Mock<ICustomerRepository>();
            mockCustRepo.Setup(x => x.Customers).Returns(new List<Customer>
            {
                new Customer{CustomerId = 1, FirstName = "Łukasz", LastName = "Testowy", Email = "lukasz@test.pl", PhoneNumber = "123456789"},
                new Customer{CustomerId = 2, FirstName = "Jan", LastName = "Testerski", Email = "jan@test.pl", PhoneNumber = "123453489"},
                new Customer{CustomerId = 3, FirstName = "Maciek", LastName = "Test", Email = "maciek@test.pl", PhoneNumber = "533456789"},
                new Customer{CustomerId = 4, FirstName = "Michał", LastName = "Tester", Email = "michal@test.pl", PhoneNumber = "753345689"}
            });

            return mockCustRepo.Object;
        }

        private static IAddressRepository GetAddressRepository()
        {
            Mock<IAddressRepository> mockAddressRepo = new Mock<IAddressRepository>();
            mockAddressRepo.Setup(x => x.Addresses).Returns(new List<Address>
            {
                new Address{ AddressId = 1, City = "Wrocław", Street = "Jakaś", BuildingNumber = "5", ApartmentNumber = "10", ZipCode = "51-100", Country = "Polska", Region = "dolnośląskie", CustomerId = 1 },
                new Address{ AddressId = 2, City = "Łódź", Street = "Wielka", BuildingNumber = "12", ApartmentNumber = "112", ZipCode = "90-100", Country = "Polska", Region = "łódzkie", CustomerId = 1 },
                new Address{ AddressId = 3, City = "Gdańsk", Street = "Krótka", BuildingNumber = "1", ApartmentNumber = "", ZipCode = "11-100", Country = "Polska", Region = "pomorskie", CustomerId = 2 },
                new Address{ AddressId = 4, City = "Poznań", Street = "Długa", BuildingNumber = "25", ApartmentNumber = "10", ZipCode = "41-100", Country = "Polska", Region = "wielkopolskie", CustomerId = 3 },
                new Address{ AddressId = 5, City = "Kraków", Street = "Wawelska", BuildingNumber = "7", ApartmentNumber = "10A", ZipCode = "31-100", Country = "Polska", Region = "małopolskie", CustomerId = 3 },
                new Address{ AddressId = 6, City = "Warszawa", Street = "Długa", BuildingNumber = "15", ApartmentNumber = "", ZipCode = "26-101", Country = "Polska", Region = "mazowieckie", CustomerId = 4 }
            });

            return mockAddressRepo.Object;
        }

        private static IOrderRepository GetOrderRepository()
        {
            List<Order> orders = new List<Order>
            {
                new Order { OrderId = 1, Items = Items.Where(i => i.OrderId == 1), AddressId = 1, CustomerId = 1, OrderNumber = "ZS/1" },
                new Order { OrderId = 2, Items = Items.Where(i => i.OrderId == 2), AddressId = 2, CustomerId = 2, OrderNumber = "ZS/2" }
            };

            Mock<IOrderRepository> mockOrderRepo = new Mock<IOrderRepository>();
            mockOrderRepo.Setup(x => x.Orders).Returns(orders);
            mockOrderRepo.Setup(x => x.SaveOrder(It.IsAny<Order>()))
            .Callback<Order>(o =>
            {
                orders.Add(o);
            });

            return mockOrderRepo.Object;
        }

        private static List<CartItem> GetCartItems()
        {
            return new List<CartItem>
            {
                new CartItem { CartItemId = 1, OrderId = 1, Product = ProductRepository.Products.First(p => p.Name == "P1"), Quantity = 1 },
                new CartItem { CartItemId = 2, OrderId = 1, Product = ProductRepository.Products.First(p => p.Name == "P2"), Quantity = 2 },
                new CartItem { CartItemId = 3, OrderId = 2, Product = ProductRepository.Products.First(p => p.Name == "P7"), Quantity = 3 },
                new CartItem { CartItemId = 4, OrderId = 2, Product = ProductRepository.Products.First(p => p.Name == "P4"), Quantity = 1 }
            };
        }

        private static IProductRepository GetProductRepository()
        {
            Mock<IProductRepository> mockProductRepo = new Mock<IProductRepository>();
            mockProductRepo.Setup(x => x.Products).Returns(new List<Product>
            {
                new Product {Name = "P1", Price = 10M, Category = "1"},
                new Product {Name = "P2", Price = 20M, Category = "1"},
                new Product {Name = "P3", Price = 30M, Category = "2"},
                new Product {Name = "P4", Price = 40M, Category = "3"},
                new Product {Name = "P5", Price = 50M, Category = "3"},
                new Product {Name = "P6", Price = 60M, Category = "3"},
                new Product {Name = "P7", Price = 70M, Category = "4"},
                new Product {Name = "P8", Price = 80M, Category = "4"}
            }.AsQueryable());

            return mockProductRepo.Object;
        }

        private static IQueryable<SportUser> GetUsers()
        {
            IEnumerable<SportUser> users = new List<SportUser>
            {
                new SportUser{ UserName = "lukasz", Email = "lukasz@test.pl", Id = "1", CustomerId = 1, PasswordHash = "Lukasz1!" },
                new SportUser{ UserName = "janek", Email = "jan@test.pl", Id = "2", CustomerId = 2, PasswordHash = "Janek12!@" },
                new SportUser{ UserName = "mac88", Email = "maciek@test.pl", Id = "3", CustomerId = 3, PasswordHash = "Maciej1!" },
                new SportUser{ UserName = "michallll", Email = "michal@test.pl", Id = "4", CustomerId = 4, PasswordHash = "Michal1!" },
                new SportUser{ UserName = "admin", Email = "admin@sportsstore.pl", Id = "5", CustomerId = 0, PasswordHash = "Admin1!" },
                new SportUser{ UserName = "sprzedawca1", Email = "sprzedawca1@sportsstore.pl", Id = "6", CustomerId = 0, PasswordHash = "Sprzedawca1!" },
            };

            IPasswordHasher<SportUser> passwordHasher = new PasswordHasher<SportUser>();
            foreach(var user in users)
            {
                user.PasswordHash = passwordHasher.HashPassword(user, user.PasswordHash);
            }

            return users.AsQueryable();
        }
    }
}
