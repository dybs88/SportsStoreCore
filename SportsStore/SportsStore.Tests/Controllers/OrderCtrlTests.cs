using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.Models.Cart;
using SportsStore.Models.Order;
using Xunit;

namespace SportsStore.Tests.Controllers
{
    public class OrderCtrlTests
    {
        private OrderController _target;
        private IOrderRepository _repo;
        public OrderCtrlTests()
        {
            List<Order> orders = new List<Order>();
            Mock<IOrderRepository> mockOrderRepo = new Mock<IOrderRepository>();
            mockOrderRepo.Setup(x => x.Orders).Returns(orders.AsQueryable());
            mockOrderRepo.Setup(x => x.SaveOrder(It.IsAny<Order>()))
                .Callback<Order>(o =>
                {
                    orders.Add(o);
                });

            _repo = mockOrderRepo.Object;
        }

        [Fact]
        public void CheckoutEmptyOrder()
        {
            //arange
            _target = new OrderController(_repo, new Cart());

            //act
            ViewResult result = (ViewResult)_target.CheckOut(new Order());

            //assert
            Assert.NotEmpty(result.ViewData.ModelState);
            Assert.False(result.ViewData.ModelState.IsValid);
            Assert.True(string.IsNullOrEmpty(result.ViewName));
        }

        [Fact]
        public void CheckCorrectOrder()
        {
            //arange
            Cart cart = new Cart();
            cart.AddItem(new Product{Price = 10}, 1);
            cart.AddItem(new Product{Price = 20}, 1);
            _target = new OrderController(_repo, cart);

            //act
            RedirectToActionResult result = (RedirectToActionResult) _target.CheckOut(new Order());
 
            //assert
            Assert.NotEmpty(_repo.Orders);
            Assert.Equal("Completed", result.ActionName);
        }
    }
}
