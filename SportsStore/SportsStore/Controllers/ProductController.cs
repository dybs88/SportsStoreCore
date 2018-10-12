using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using SportsStore.Controllers.Base;
using SportsStore.DAL.Repos;
using SportsStore.DAL.Repos.DictionarySchema;
using SportsStore.DAL.Repos.Security;
using SportsStore.Domain;
using SportsStore.Helpers;
using SportsStore.Models;
using SportsStore.Models.DictionaryModels;
using SportsStore.Models.Parameters;
using SportsStore.Models.ProductModels;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class ProductController : BaseController
    {
        private IProductRepository _productRepository;
        private IDictionaryContainer _dictionaryContainer;
        private ISystemParameterRepository _paramRepository;
        private int _pageSize = 4;

        public ProductController(IServiceProvider provider, IConfiguration config, IProductRepository repository, IDictionaryContainer dictContainer, ISystemParameterRepository paramRepo)
            : base(provider, config)
        {
            _productRepository = repository;
            _dictionaryContainer = dictContainer;
            _paramRepository = paramRepo;
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
            return View("EditProduct", new ProductEditViewModel { VatRates = _dictionaryContainer.VatRateRepository.VatRates });
        }
        [Authorize]
        public IActionResult EditProduct(int productId)
        {
            return View(new ProductEditViewModel
            {
                Product = _productRepository.GetProduct(productId),
                VatRates = _dictionaryContainer.VatRateRepository.VatRates,
                DefaultPriceType = (PriceType)Enum.Parse(typeof(PriceType),_paramRepository.GetParameter(SystemParameters.ProductPriceType).Value)
            });
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditProduct(ProductEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                _productRepository.SaveProduct(model);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProductImages(List<ProductImage> images)
        {
            if(images == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NoContent;
                return Json("Błąd - brak danych");
            }

            _productRepository.DeleteProductImages(images);
            return Json("Usuwanie zakończone");
        }

        [HttpGet]
        public IActionResult GetVatRate(int vatRateId)
        {
            return Json(_dictionaryContainer.VatRateRepository.GetVatRate(vatRateId));
        }
    }
}