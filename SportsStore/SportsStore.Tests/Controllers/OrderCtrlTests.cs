using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.Models.Base;
using SportsStore.Models.Cart;
using SportsStore.Models.CustomerModels;
using SportsStore.Models.DAL.Repos.SalesSchema;
using SportsStore.Models.OrderModels;
using SportsStore.Models.ProductModels;
using SportsStore.Tests.Base;
using Xunit;

namespace SportsStore.Tests.Controllers
{
    public class OrderCtrlTests
    {
        private OrderController _target;
        public OrderCtrlTests()
        {
            _target = new OrderController(MockedObjects.Provider,
                                          MockedObjects.Configuration,
                                          Repositories.OrderRepository, 
                                          new Cart(), 
                                          Repositories.CustomerRepository, 
                                          Repositories.AddressRepository);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void CanGetOrders(int customerId)
        {
            //act
            ViewResult result = (ViewResult)_target.List(customerId);
            OrderListViewModel model = (OrderListViewModel)result.Model;

            //assert
            Assert.NotEmpty(model.Orders);
            Assert.True(result.ViewData.ModelState.IsValid);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        public void CanGetSelectedOrderByOrderId(string orderId)
        {
            //arrange
            BaseListViewModel model = new BaseListViewModel { SearchData = orderId };

            //act
            ViewResult result = (ViewResult)_target.List(model);
            OrderListViewModel resultModel = (OrderListViewModel)result.Model;

            //assert
            Assert.Single(resultModel.Orders);
            Assert.True(result.ViewData.ModelState.IsValid);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("4")]
        public void CanGetSelectedOrderByOrderNumber(string orderNumber)
        {
            //arrange
            BaseListViewModel model = new BaseListViewModel { SearchData = orderNumber };

            //act
            ViewResult result = (ViewResult)_target.List(model);
            OrderListViewModel resultModel = (OrderListViewModel)result.Model;

            //assert
            Assert.Single(resultModel.Orders);
            Assert.True(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void CanCreateNewOrder()
        {
            //arrange
            #region arrange
            CreateOrderViewModel model = new CreateOrderViewModel();
            int customerId = 3;
            CustomerFullData customerFullData = new CustomerFullData
            {
                Customer = Repositories.CustomerRepository.Customers.FirstOrDefault(c => c.CustomerId == customerId),
                Addresses = Repositories.AddressRepository.Addresses.Where(a => a.CustomerId == customerId)
            };

            model.CustomerFullData = customerFullData;
            model.SelectedAddress = customerFullData.Addresses.First();

            Cart orderCart = new Cart();
            orderCart.AddItem(Repositories.ProductRepository.Products.First(p => p.Name == "P1"), 1);
            orderCart.AddItem(Repositories.ProductRepository.Products.First(p => p.Name == "P3"), 3);
            _target.SetCart(orderCart);
            
            #endregion

            //act
            RedirectToActionResult result = (RedirectToActionResult)_target.CreateOrder(model).Result;

            //assert
            Assert.Equal("Completed", result.ActionName);
        }
    }
}
