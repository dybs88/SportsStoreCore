using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Controllers
{
    public class BaseController : Controller
    {
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
