using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Controllers.Base;
using SportsStore.DAL.Repos.CustomerSchema;
using SportsStore.Domain;
using SportsStore.Infrastructure.Abstract;
using SportsStore.Infrastructure.Extensions;
using SportsStore.Models.Base;
using SportsStore.Models.Cart;
using SportsStore.Models.CustomerModels;
using SportsStore.Models.DAL.Repos.SalesSchema;
using SportsStore.Models.Identity;
using SportsStore.Models.OrderModels;

namespace SportsStore.Controllers
{
    [Authorize]
    public class OrderController : BaseController, ISearchable
    {
        private IOrderRepository _orderRepository;
        private Cart _cart;
        private UserManager<SportUser> _userManager;
        private ICustomerRepository _customerRepository;
        private IAddressRepository _addressRepository;

        public OrderController(IOrderRepository repo, 
                               Cart cart, 
                               UserManager<SportUser> userManager, 
                               ICustomerRepository custRepo, 
                               IAddressRepository addressRepo)
        {
            _orderRepository = repo;
            _cart = cart;
            _userManager = userManager;
            _customerRepository = custRepo;
            _addressRepository = addressRepo;
        }

        [Authorize(Policy = SalesPermissionValues.ViewOrder)]
        public async Task<IActionResult> List(int currentPage = 1)
        {
            int customerId = 0;
            if (RouteData.Values["customerId"] != null)
                customerId = int.Parse(RouteData.Values["customerId"].ToString());

            ViewBag.CustomerId = customerId;

            var orders = PaginateList(_orderRepository.Orders, currentPage);

            OrderListViewModel model = new OrderListViewModel(customerId, orders, currentPage, _pageSize, _orderRepository.Orders.Count());
            return View(model);
        }

        [Authorize(Policy = SalesPermissionValues.ViewOrder)]
        [HttpPost]
        public async Task<IActionResult> List(BaseListViewModel model)
        {
            if(model == null || string.IsNullOrEmpty(model.SearchData))
            {
                return await List();
            }
            else
            {
                int customerId = 0;
                if (RouteData.Values["customerId"] != null)
                    customerId = int.Parse(RouteData.Values["customerId"].ToString());

                ViewBag.CustomerId = customerId;

                List<Order> orders = new List<Order>();
                int orderId = 0;

                if (int.TryParse(model.SearchData, out orderId))
                    orders.Add(_orderRepository.GetOrder(orderId));
                else
                    orders.Add(_orderRepository.GetOrderByNumber(model.SearchData));

                OrderListViewModel orderModel = new OrderListViewModel(customerId, orders, 1, _pageSize, orders.Count());
                return View(orderModel);
            }
        }

        [Authorize(Policy = SalesPermissionValues.EditOrder)]
        public IActionResult Edit(int customerId, int orderId)
        {
            ViewBag.CustomerId = customerId;
            return View(_orderRepository.GetOrder(orderId));
        }

        [HttpPost]
        [Authorize(Policy = SalesPermissionValues.EditOrder)]
        public IActionResult MarkShipped(int orderId)
        {
            var order = _orderRepository.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order != null)
            {
                order.Shipped = true;
                _orderRepository.SaveOrder(order);
            }

            return RedirectToAction("List");
        }

        [Authorize(Policy = SalesPermissionValues.AddOrder)]
        public async Task<IActionResult> CreateOrder()
        {
            if (_cart.Items.Count == 0)
            {
                ModelState.AddModelError("", "Koszyk jest pusty");
                return Redirect("Cart/Index");
            }
            if (!User.Identity.IsAuthenticated)
                return Redirect("Account/Login");

            var customerId = await _userManager.GetCustomerIdByNameAsync<SportUser>(User.Identity.Name);
            return View(new CreateOrderViewModel { CustomerFullData = await _customerRepository.GetCustomerFullData(customerId) });
        }

        [HttpPost]
        [Authorize(Policy = SalesPermissionValues.AddOrder)]
        public async Task<IActionResult> CreateOrder(CreateOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                Order newOrder = _orderRepository.CreateNewOrder(model.CustomerFullData.Customer.CustomerId);
                newOrder.Address = model.SelectedAddress;
                newOrder.Items.AddRange(_cart.Items);
                _orderRepository.SaveOrder(newOrder);
                return RedirectToAction("Completed", new { order = newOrder });
            }
            else
            {
                model.CustomerFullData = await _customerRepository.GetCustomerFullData(model.CustomerFullData.Customer.CustomerId);
                return View(model);
            }
        }

        public ViewResult Completed(Order order)
        {
            _cart.ClearCart();
            return View(order);
        }
    }
}