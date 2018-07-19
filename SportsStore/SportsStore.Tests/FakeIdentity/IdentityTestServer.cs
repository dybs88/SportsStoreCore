using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace SportsStore.Tests.FakeIdentity
{
    public class IdentityTestServer<TStartup> : IDisposable where TStartup : class
    {
        public readonly TestServer Server;
        private readonly HttpClient _client;


        public IdentityTestServer()
        {
            var builder = new WebHostBuilder()
                .UseContentRoot(@"D:\Git\SportsStoreCore\SportsStore\SportsStore")
                .UseStartup<TStartup>();

            Server = new TestServer(builder);
            _client = new HttpClient();
        }


        public void Dispose()
        {
            _client.Dispose();
            Server.Dispose();
        }
    }
}
