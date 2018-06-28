using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models.Cart;
using SportsStore.Models.Order;

namespace SportsStore.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository _repository;
        private Cart _cart;

        public OrderController(IOrderRepository repo, Cart cart)
        {
            _repository = repo;
            _cart = cart;
        }

        [Authorize]
        public ViewResult List(bool filterByShipped = false)
        {
            return View(filterByShipped ? _repository.Orders.Where(o => o.Shipped == false) : _repository.Orders);
        }

        [HttpPost]
        [Authorize]
        public IActionResult MarkShipped(int orderId)
        {
            var order = _repository.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order != null)
            {
                order.Shipped = true;
                _repository.SaveOrder(order);
            }

            return RedirectToAction("List");
        }

        public ViewResult CheckOut()
        {
            return View(new Order());
        }

        [HttpPost]
        public IActionResult CheckOut(Order order)
        {
            if (_cart.Items.Count == 0)
            {
                ModelState.AddModelError("","Koszyk jest pusty");
            }

            if (ModelState.IsValid)
            {
                order.Items = _cart.Items.ToArray();
                _repository.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }

        public ViewResult Completed()
        {
            _cart.ClearCart();
            return View();
        }
    }
}