using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;
using static SportsStore.Startup;

namespace SportsStore.Tests.Base
{
    public class SportsStoreBaseTest : IClassFixture<WebApplicationFactory<TestStartup>>
    {
        private readonly WebApplicationFactory<TestStartup> _factory;
        private HttpClient _target;

        public SportsStoreBaseTest(WebApplicationFactory<TestStartup> factory)
        {
            _factory = factory;
            _target = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async void RunTestClient()
        {
            var response = await _target.GetAsync("/Product/List");
            // Assert
            //Assert.Equal(HttpStatusCode.Redirect, response.Status.ToString());
            //Assert.StartsWith("http://localhost/Identity/Account/Login",
               // response.Headers.Location.OriginalString);
        }
    }
}
