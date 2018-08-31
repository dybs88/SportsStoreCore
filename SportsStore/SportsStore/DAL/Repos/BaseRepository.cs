using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.DAL.Repos
{
    public abstract class BaseRepository
    {
        protected ISession _session;
        public BaseRepository(IServiceProvider provider)
        {
            _session = provider.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
        }
    }
}
