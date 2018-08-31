using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Controllers.Base;
using SportsStore.DAL.Repos;
using SportsStore.Helpers;
using SportsStore.Models;
using SportsStore.Models.ProductModels;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class ProductController : BaseController
    {
        private IProductRepository _repository;
        private int _pageSize = 4;

        public ProductController(IServiceProvider provider, IProductRepository repository)
            : base(provider)
        {
            _repository = repository;
        }

        public ViewResult List(string category, int currentPage = 1)
        {
            var paginateProducts = _repository.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductID)
                .Skip((currentPage - 1) * _pageSize)
                .Take(_pageSize);

            ProductListViewModel model = new ProductListViewModel(paginateProducts,_repository.Products.Count(p => category == null || p.Category == category), _pageSize, currentPage, category);
            return View(model);
        }

        [Authorize]
        public ViewResult Products()
        {
            return View(_repository.Products);
        }
        [Authorize]
        public ViewResult CreateProduct()
        {
            return View("EditProduct", new Product());
        }
        [Authorize]
        public ViewResult EditProduct(int productId)
        {
            var product = _repository.Products.FirstOrDefault(p => p.ProductID == productId);
            return View(product);
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveProduct(product);
                TempData["message"] = $"Zapisano {product.Name}";
                return RedirectToAction("Products");
            }
            else
                return View(product);
        }
        [Authorize]
        [HttpPost]
        public IActionResult DeleteProduct(int productId)
        {
            var deletedProduct = _repository.DeleteProduct(productId);
            TempData["message"] = $"Usunięto produkt {deletedProduct.Name}";
            return RedirectToAction("Products");
        }
    }
}