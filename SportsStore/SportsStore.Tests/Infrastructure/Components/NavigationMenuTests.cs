using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using SportsStore.DAL.Repos;
using SportsStore.Infrastructure.Components;
using SportsStore.Models;
using SportsStore.Models.ProductModels;
using SportsStore.Models.ViewModels;
using SportsStore.Tests.Base;
using Xunit;

namespace SportsStore.Tests
{
    public class NavigationMenuTests
    {
        private NavigationMenuViewComponent target;

        public NavigationMenuTests()
        {
            target = new NavigationMenuViewComponent(Repositories.ProductRepository);
        }

        [Fact]
        public void GetDistinctCategories()
        {
            //arange

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
