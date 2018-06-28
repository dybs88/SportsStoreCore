using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using SportsStore.Infrastructure.Components;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
using Xunit;

namespace SportsStore.Tests
{
    public class NavigationMenuTests
    {
        private NavigationMenuViewComponent target;

        public void Initialize()
        {
            Mock<IProductRepository> mockRepository = new Mock<IProductRepository>();
            mockRepository.Setup(x => x.Products).Returns(() => new List<Product>
            {
                new Product {Name = "Produkt 1", Price = 10M, Category = "C"},
                new Product {Name= "Produckt 2", Price = 20M, Category = "B"},
                new Product {Name = "Produkt 3", Price = 30M, Category = "B"},
                new Product {Name = "Produkt 4", Price = 40M, Category = "D"},
                new Product {Name = "Produkt 5", Price = 50M, Category = "A"}
            }.AsQueryable());

            target = new NavigationMenuViewComponent(mockRepository.Object);
        }

        [Fact]
        public void GetDistinctCategories()
        {
            //arange
            Initialize();

            //act
            var result = (NavigationMenuViewModel)(target.Invoke() as ViewViewComponentResult).ViewData.Model;
            
            //assert
            Assert.Equal("A", result.Categories.ToArray()[0]);
            Assert.Equal("B", result.Categories.ToArray()[1]);
            Assert.Equal("C", result.Categories.ToArray()[2]);
            Assert.Equal("D", result.Categories.ToArray()[3]);
        }
    }
}
