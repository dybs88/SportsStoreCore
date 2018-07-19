using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Moq;
using SportsStore.DAL.Repos.CustomerSchema;
using SportsStore.Models.CustomerModels;
using SportsStore.Models.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace SportsStore.Tests
{
    public class Repositories
    {
        public static ICustomerRepository CustomerRepository => GetCustomerRepository();

        public static IAddressRepository AddressRepository => GetAddressRepository();

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
