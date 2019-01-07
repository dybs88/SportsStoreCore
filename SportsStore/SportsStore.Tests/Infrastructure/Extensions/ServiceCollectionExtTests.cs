using Castle.Core.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using SportsStore.Helpers;
using SportsStore.Infrastructure.Extensions;
using SportsStore.Tests.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SportsStore.Tests.Infrastructure.Extensions
{
    public class ServiceCollectionExtTests
    {
        IServiceCollection _target;

        public ServiceCollectionExtTests()
        {
            _target = MockedObjects.Services;

        }

        [Fact]
        public void CanGetAppSettings()
        {
            //arrange
            _target.AddAppSettings(MockedObjects.Configuration);

            //act
            var settings = _target.GetAppSettings(MockedObjects.Configuration);

            //assert
            Assert.Equal("Testowy sekret", settings.Secret);
            Assert.Equal("http://localhost:30000", settings.WebClientAddress);
        }
    }
}
