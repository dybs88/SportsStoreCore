using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using SportsStore.Controllers;
using SportsStore.DAL.Repos;
using SportsStore.Models;
using SportsStore.Models.ProductModels;
using SportsStore.Models.ViewModels;
using Xunit;

namespace SportsStore.Tests
{
    public class ProductCtrlTests
    {
        private ProductController target;

        public void Initialize()
        {
           Mock<IProductRepository> mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(x => x.Products).Returns(() => new List<Product>
            {
                new Product {Name = "P1", Price = 10M, Category = "1"},
                new Product {Name = "P2", Price = 20M, Category = "1"},
                new Product {Name = "P3", Price = 30M, Category = "2"},
                new Product {Name = "P4", Price = 40M, Category = "3"},
                new Product {Name = "P5", Price = 50M, Category = "3"},
                new Product {Name = "P6", Price = 60M, Category = "3"},
                new Product {Name = "P7", Price = 70M, Category = "4"},
                new Product {Name = "P8", Price = 80M, Category = "4"}
            }.AsQueryable());

            target = new ProductController(mockRepository.Object);
        }

        [Fact]
        public void PaginateProducts()
        {
            //arange
            Initialize();

            //act
            var result = (ProductListViewModel)target.List(null, 2).ViewData.Model;

            //assert
            Assert.True(result.Products.First().Name == "P5");
            Assert.True(result.Products.Count() == 4);
        }

        [Fact]
        public void PageHelperTest()
        {
            //arange
            Initialize();

            //act
            var result = (ProductListViewModel) target.List(null, 2).ViewData.Model;

            //assert
            Assert.Equal(8, result.PageHelper.TotalItems);
            Assert.Equal(2, result.PageHelper.CurrentPage);
            Assert.Equal(4, result.PageHelper.ItemPerPage);
            Assert.Equal(2, result.PageHelper.TotalPages);
        }

        [Fact]
        public void FilterProductsByCategory()
        {
            //arange
            Initialize();

            //act
            var result = (ProductListViewModel) target.List("3", 1).ViewData.Model;

            //assert
            Assert.Equal(3, result.Products.Count());
            Assert.True(result.Products.First().Category == "3");
        }

        [Fact]
        public void CountFilteredProductsByCategory()
        {
            //arange
            Initialize();

            //act
            var result = (ProductListViewModel) target.List("2", 1).ViewData.Model;

            //assert
            Assert.True(result.PageHelper.TotalItems == 1);
            Assert.True(result.PageHelper.TotalPages == 1);
        }
    }
}
