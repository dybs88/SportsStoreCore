using System;
using System.Collections.Generic;
using System.IO;
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
        private IProductRepository _productRepository;
        private int _pageSize = 4;

        public ProductController(IServiceProvider provider, IProductRepository repository)
            : base(provider)
        {
            _productRepository = repository;
        }

        public IActionResult Index(int productId)
        {
            return View(new ProductIndexViewModel { Product = _productRepository.GetProduct(productId) });
        }

        public ViewResult List(string category, int currentPage = 1)
        {
            var paginateProducts = _productRepository.Products
                .Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductID)
                .Skip((currentPage - 1) * _pageSize)
                .Take(_pageSize);

            ProductListViewModel model = new ProductListViewModel(paginateProducts,_productRepository.Products.Count(p => category == null || p.Category == category), _pageSize, currentPage, category);
            return View(model);
        }

        [Authorize]
        public ViewResult Products()
        {
            return View(_productRepository.Products);
        }
        [Authorize]
        public ViewResult CreateProduct()
        {
            return View("EditProduct", new Product());
        }
        [Authorize]
        public ViewResult EditProduct(int productId)
        {
            var product = _productRepository.Products.FirstOrDefault(p => p.ProductID == productId);
            return View(new ProductEditViewModel { Product = product });
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditProduct(ProductEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                //foreach(var file in model.Files.Distinct(f => f.FileName))
                //{
                //    var filePath = "images\" + file.Name;
                //    if(file.Length > 0)
                //    {
                //        using (var stream = new FileStream(filePath, FileMode.Create))
                //            file.CopyTo(stream);
                //    }
                //}

                _productRepository.SaveProduct(model.Product);
                TempData["message"] = $"Zapisano {model.Product.Name}";
                return RedirectToAction("Products");
            }
            else
                return View(model);
        }
        [Authorize]
        [HttpPost]
        public IActionResult DeleteProduct(int productId)
        {
            var deletedProduct = _productRepository.DeleteProduct(productId);
            TempData["message"] = $"Usunięto produkt {deletedProduct.Name}";
            return RedirectToAction("Products");
        }
    }
}