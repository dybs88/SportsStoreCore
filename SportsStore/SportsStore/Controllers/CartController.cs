using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SportsStore.DAL.Repos;
using SportsStore.Infrastructure.Extensions;
using SportsStore.Models;
using SportsStore.Models.Cart;
using SportsStore.Models.ProductModels;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository _repository;
        private Cart _cart;

        public CartController(IProductRepository repo, Cart cart)
        {
            _repository = repo;
            _cart = cart;
        }

        public IActionResult Index(string returnUrl)
        {
            CartViewModel model = new CartViewModel
            {
                Cart = _cart,
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = _repository.Products.First(p => p.ProductID == productId);
            if (product != null)
            {
                _cart.AddItem(product,1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = _repository.Products.First(p => p.ProductID == productId);

            if (product != null)
            {
                _cart.RemoveItem(product, 1);
            }

            return RedirectToAction("Index", new {returnUrl});
        }
    }
}