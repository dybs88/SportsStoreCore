using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Domain;
using SportsStore.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace SportsStore.Controllers.Base
{
    public class BaseController : Controller
    {
        private IServiceProvider _provider;
        protected int _pageSize = 10;
        protected ISession _session;
        protected IConfiguration _configuration;

        public BaseController(IServiceProvider provider, IConfiguration config)
        {
            _provider = provider;
            _session = provider.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            _configuration = config;
        }

        protected IEnumerable<T> PaginateList<T>(IEnumerable<T> list, int currentPage) where T : class
        {
            return list.Skip((currentPage - 1) * _pageSize).Take(_pageSize);
        }


        protected void AddErrors(IEnumerable<IdentityError> errors)
        {
            foreach(var error in errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
