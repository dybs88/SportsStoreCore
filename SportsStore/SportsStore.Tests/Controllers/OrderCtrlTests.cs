using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.Models.Cart;
using SportsStore.Models.DAL.Repos.SalesSchema;
using SportsStore.Models.OrderModels;
using SportsStore.Models.ProductModels;
using Xunit;

namespace SportsStore.Tests.Controllers
{
    public class OrderCtrlTests
    {
        private OrderController _target;
        public OrderCtrlTests()
        {
        }

        [Fact]
        public void ShowOrderList()
        {
            //arange
            _target = new OrderController(Repositories.OrderRepository, new Cart(), null, Repositories.CustomerRepository, Repositories.AddressRepository);

            //act
            ViewResult result = (ViewResult)_target.List().Result;

            ////assert
            //Assert.NotEmpty(result.ViewData.ModelState);
            //Assert.False(result.ViewData.ModelState.IsValid);
            //Assert.True(string.IsNullOrEmpty(result.ViewName));
        }

        [Fact]
        public void CheckCorrectOrder()
        {
            ////arange
            //Cart cart = new Cart();
            //cart.AddItem(new Product{Price = 10}, 1);
            //cart.AddItem(new Product{Price = 20}, 1);
            //_target = new OrderController(_repo, cart);

            ////act
            //RedirectToActionResult result = (RedirectToActionResult) _target.CreateOrder(new Order());
 
            ////assert
            //Assert.NotEmpty(_repo.Orders);
            //Assert.Equal("Completed", result.ActionName);
        }
    }
}
