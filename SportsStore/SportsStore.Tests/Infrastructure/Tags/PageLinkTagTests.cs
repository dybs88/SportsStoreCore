using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using SportsStore.Helpers;
using SportsStore.Infrastructure.Tags;
using Xunit;

namespace SportsStore.Tests
{
    public class PageLinkTagTests
    {
        private PageLinkTag target;
        private TagHelperContext context;
        private TagHelperOutput output;

        public void Initialize()
        {
            Mock<IUrlHelper> mockUrlHelper = new Mock<IUrlHelper>();
            mockUrlHelper.SetupSequence(x => x.Action(It.IsAny<UrlActionContext>()))
                .Returns("Test/Page1")
                .Returns("Test/Page2")
                .Returns("Test/Page3");

            Mock<IUrlHelperFactory> mockFactory = new Mock<IUrlHelperFactory>();
            mockFactory.Setup(x => x.GetUrlHelper(It.IsAny<ActionContext>())).Returns(mockUrlHelper.Object);

            target = new PageLinkTag(mockFactory.Object)
            {
                PageModel = new PageHelper
                {
                    TotalItems = 28,
                    CurrentPage = 2,
                    ItemPerPage = 10
                }
            };

            context = new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(),"");

            Mock<TagHelperContent> mockContent = new Mock<TagHelperContent>();
            
            output = new TagHelperOutput("div",new TagHelperAttributeList(),(cache,encoder) => Task.FromResult(mockContent.Object));
        }

        [Fact]
        public void GeneratePageLinks()
        {
            //arange
            Initialize();

            //act
            target.Process(context,output);
            var result = output.Content.GetContent();
            //assert
            Assert.Equal(@"<div><a class="" "" href=""Test/Page1"">1</a><a class="" "" href=""Test/Page2"">2</a><a class="" "" href=""Test/Page3"">3</a></div>", result);
        }
    }
}
