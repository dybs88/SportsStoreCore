using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SportsStore.DAL.Repos
{
    public abstract class BaseRepository
    {
        protected ISession _session;
        protected IConfiguration _configuration;

        public BaseRepository(IServiceProvider provider, IConfiguration config)
        {
            _session = provider.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            _configuration = config;
        }
    }
}
