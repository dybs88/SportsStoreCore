using SportsStore.DAL.Repos.CustomerSchema;
using SportsStore.Models.CustomerModels;
using SportsStore.Tests.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SportsStore.Tests.RepositoriesTests
{
    public class ToSaveCustomerData<T> : TheoryData<T>
    {
        public ToSaveCustomerData(IEnumerable<T> datas)
        {
            foreach (T data in datas)
                Add(data);
        }
    }

    public class CustomerRepoTests
    {
        public ICustomerRepository _target;
        TestSession _session;

        public CustomerRepoTests()
        {
            _target = new CustomerRepository(MockedObjects.ApplicationDbContext, MockedObjects.UserManager, Repositories.AddressRepository, Repositories.OrderRepository);
            _session = (TestSession)MockedObjects.Session;
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void CanGetCustomerById(int customerId)
        {
            //arrange

            //act
            Customer result = _target.GetCustomer(customerId);

            //assert
            Assert.NotNull(result);
            Assert.Equal(customerId, result.CustomerId);
            Assert.NotEmpty(result.Email);
            Assert.NotEmpty(result.FirstName);
            Assert.NotEmpty(result.LastName);
            Assert.NotEmpty(result.PhoneNumber);
        }

        [Theory]
        [InlineData("lukasz@test.pl")]
        [InlineData("jan@test.pl")]
        [InlineData("maciek@test.pl")]
        [InlineData("michal@test.pl")]
        public void CanGetCustomerByEmail(string email)
        {
            //arrange

            //act
            Customer result = _target.GetCustomer(email);

            //assert
            Assert.NotNull(result);
            Assert.Equal(email, result.Email);
            Assert.NotEqual(0, result.CustomerId);
            Assert.NotEmpty(result.FirstName);
            Assert.NotEmpty(result.LastName);
            Assert.NotEmpty(result.PhoneNumber);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void CanGetAdditionalData(int customerId)
        {
            //arrange

            //act
            CustomerAdditionalData result = _target.GetCustomerAdditionalData(customerId);

            //assert
            Assert.Equal(customerId, result.CustomerId);
            Assert.NotEqual(0, result.CustomerOrdersCount);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public async void CanGetCustomerFullData(int customerId)
        {
            //arrange

            //act
            CustomerFullData result = await _target.GetCustomerFullData(customerId);

            //assert
            Assert.NotNull(result.Customer);
            Assert.NotNull(result.User);
            Assert.NotNull(result.Addresses);
            Assert.NotNull(result.AdditionalData);
            Assert.Equal(customerId, result.Customer.CustomerId);
            Assert.Equal(customerId, result.User.CustomerId);
            Assert.NotEmpty(result.Addresses);
            Assert.Equal(customerId, result.AdditionalData.CustomerId);
            Assert.Equal(result.Customer.Email, result.User.Email);
        }


        private static IEnumerable<Customer> _customerToSave = new List<Customer>
        {
            new Customer { CustomerId = 0, FirstName = "Dariusz", LastName = "Programowy", Email = "darrreo@test.pl", PhoneNumber = "554623235"},
            new Customer { CustomerId = 2, FirstName = "Jan", LastName = "Testerski", Email = "jan_nowy_mail@test.pl", PhoneNumber = "987654321"},
        };
        public static ToSaveCustomerData<Customer> customerToSaves => new ToSaveCustomerData<Customer>(_customerToSave);

        [Theory]
        [MemberData(nameof(customerToSaves))]
        public void CanSaveCustomer(Customer customer)
        {
            //arrange

            //act
            int result = _target.SaveCustomer(customer);
            Customer savedCustomer = _target.GetCustomer(result);

            //assert
        }
    }
}
