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

namespace SportsStore.Controllers.Base
{
    public class BaseController : Controller
    {
        protected ISession _session;
        private IServiceProvider _provider;
        protected int _pageSize = 10;
        public BaseController(IServiceProvider provider)
        {
            _provider = provider;
            _session = provider.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
        }

        public async Task<IActionResult> Start()
        {
            if (User.Identity.IsAuthenticated)
            {
                byte[] outVal = new List<byte>().ToArray();
                if (!(_session.TryGetValue(SessionData.CustomerId, out outVal)))
                {
                    var userManager = _provider.GetRequiredService<UserManager<SportUser>>();
                    var loggedUSer = await userManager.FindByNameAsync(User.Identity.Name);
                    _session.SetInt32(SessionData.CustomerId, loggedUSer.CustomerId);
                }
            }
            return RedirectToAction("List", "Product");
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
