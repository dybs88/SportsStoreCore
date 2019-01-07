using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SportsStore.DAL.Repos;
using SportsStore.DAL.Repos.DictionarySchema;
using SportsStore.DAL.Repos.Security;
using SportsStore.Models.ProductModels;

namespace SportsStore.ApiControllers
{
    [Produces("application/json")]
    [Route("api/products")]
    public class ProductApiController : Controller
    {
        private IProductRepository _productRepository;
        private IDictionaryContainer _dictionaryContainer;
        private ISystemParameterRepository _paramRepository;

        public ProductApiController(IProductRepository repository, IDictionaryContainer dictContainer, ISystemParameterRepository paramRepo)
        {
            _productRepository = repository;
            _dictionaryContainer = dictContainer;
            _paramRepository = paramRepo;
        }

        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            return _productRepository.Products;
        }

        [HttpPost]
        public Product SaveProduct(Product product)
        {
            ProductEditViewModel model = new ProductEditViewModel { Product = product };
            var savedProduct = _productRepository.SaveProduct(model);
            return savedProduct;
        }

        [HttpDelete("{productId}")]
        public void DeleteProduct(int productId)
        {
            _productRepository.DeleteProduct(productId);
        }
    }
}