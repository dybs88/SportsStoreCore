using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Controllers.Base
{
    public class BaseController : Controller
    {
        protected int _pageSize = 10;

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
