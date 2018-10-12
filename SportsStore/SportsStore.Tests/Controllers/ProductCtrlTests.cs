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
using SportsStore.Tests.Base;
using Xunit;

namespace SportsStore.Tests
{
    public class ProductCtrlTests
    {
        private ProductController target;

        public ProductCtrlTests()
        {
            target = new ProductController(MockedObjects.Provider, MockedObjects.Configuration, Repositories.ProductRepository, MockedObjects.DictionaryContainer);
        }

        [Fact]
        public void PaginateProducts()
        {
            //act
            var result = (ProductListViewModel)target.List(null, 2).ViewData.Model;

            //assert
            Assert.True(result.Products.First().Name == "P5");
            Assert.True(result.Products.Count() == 4);
        }

        [Fact]
        public void PageHelperTest()
        {
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

            //act
            var result = (ProductListViewModel) target.List("2", 1).ViewData.Model;

            //assert
            Assert.True(result.PageHelper.TotalItems == 1);
            Assert.True(result.PageHelper.TotalPages == 1);
        }
    }
}
