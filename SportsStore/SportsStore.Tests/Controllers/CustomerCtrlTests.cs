using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using SportsStore.Controllers;
using SportsStore.DAL.Repos.CustomerSchema;
using SportsStore.Models.Base;
using SportsStore.Models.CustomerModels;
using SportsStore.Models.Identity;
using SportsStore.Tests.Base;
using SportsStore.Tests.FakeIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SportsStore.Tests.Controllers
{
    public class CustomerMemberData<T> : TheoryData<T>
    {
        public CustomerMemberData(IEnumerable<T> datas)
        {
            foreach (var data in datas)
                Add(data);
        }
    }

    public class CustomerCtrlTests
    {
        CustomerController _target;

        public CustomerCtrlTests()
        {
            _target = new CustomerController(MockedObjects.Provider, Repositories.CustomerRepository, Repositories.AddressRepository, Repositories.OrderRepository);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void CanGetPageWithCustomers(int pageNumber)
        {
            //arrange

            //act
            ViewResult result =  (ViewResult)await _target.List(pageNumber);
            CustomerListViewModel model = (CustomerListViewModel)result.Model;

            //assert
            Assert.Equal(Repositories.CustomerRepository.Customers.Count(), model.PageModel.TotalItems);
            Assert.Equal(pageNumber, model.PageModel.CurrentPage);
            Assert.NotEmpty(model.Customers);
        }

        [Theory]
        [InlineData(3)]
        public async void CanGetEmptyPage(int pageNumber)
        {
            ViewResult result = (ViewResult)await _target.List(pageNumber);
            CustomerListViewModel model = (CustomerListViewModel)result.Model;

            //assert
            Assert.Equal(Repositories.CustomerRepository.Customers.Count(), model.PageModel.TotalItems);
            Assert.Equal(pageNumber, model.PageModel.CurrentPage);
            Assert.Empty(model.Customers);
        }

        private static IEnumerable<BaseListViewModel> canGetCustomerById_listModels = new List<BaseListViewModel>
        {
            new BaseListViewModel { SearchData = "1"},
            new BaseListViewModel { SearchData = "3"},
            new BaseListViewModel { SearchData = "6"}
        };

        public static CustomerMemberData<BaseListViewModel> canGetCustomerById_Datas = new CustomerMemberData<BaseListViewModel>(canGetCustomerById_listModels);

        [Theory]
        [MemberData(nameof(canGetCustomerById_Datas))]
        public async void CanGetCustomerById(BaseListViewModel inputModel)
        {
            //arrange

            //act
            ViewResult result = (ViewResult)await _target.List(inputModel);
            CustomerListViewModel resultModel = (CustomerListViewModel)result.Model;

            //assert
            Assert.NotNull(resultModel);
            Assert.Equal(1, resultModel.Customers.Count);
            Assert.Equal(int.Parse(inputModel.SearchData), resultModel.Customers.First().Customer.CustomerId);
        }

        private static IEnumerable<BaseListViewModel> canGetCustomerByEmail_listModels = new List<BaseListViewModel>
        {
            new BaseListViewModel { SearchData = "ugly_man@test.pl"},
            new BaseListViewModel { SearchData = "maciek@test.pl"},
            new BaseListViewModel { SearchData = "polityk@test.pl"}
        };

        public static CustomerMemberData<BaseListViewModel> canGetCustomerByEmail_Datas = new CustomerMemberData<BaseListViewModel>(canGetCustomerByEmail_listModels);

        [Theory]
        [MemberData(nameof(canGetCustomerByEmail_Datas))]
        public async void CanGetCustomerByEmail(BaseListViewModel inputModel)
        {
            //arrange

            //act
            ViewResult result = (ViewResult)await _target.List(inputModel);
            CustomerListViewModel resultModel = (CustomerListViewModel)result.Model;

            //assert
            Assert.NotNull(resultModel);
            Assert.Equal(1, resultModel.Customers.Count);
            Assert.Equal(inputModel.SearchData, resultModel.Customers.First().Customer.Email);
        }
    }
}
