using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class UserController : Controller
    {
        private UserManager<IdentityUser> _userManager;

        public UserController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult List()
        {
            return View(_userManager.Users);
        }

        public IActionResult CreateUser()
        {
            return View(new CreateUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =await _userManager.CreateAsync(model.User, model.Password);
                if(result.Succeeded)
                    return RedirectToAction("List");
                else
                    foreach (var identityError in result.Errors)
                    {
                        ModelState.AddModelError("",identityError.Description);
                    }
            }

            return View("CreateUser", model);        
        }
    }
}
