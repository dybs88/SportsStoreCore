using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult List()
        {
            return View(_roleManager.Roles.ToList());
        }

        public IActionResult CreateRole()
        {
            return View(new IdentityRole());
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(IdentityRole role)
        {
            if(ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("List");
                else
                    result.Errors.ToList().ForEach(e => { ModelState.AddModelError("", e.Description); });
            }
            return View();
        }

        public IActionResult EditRole(string roleName)
        {
            return View();
        }

        public IActionResult EditRole(IdentityRole role)
        {
            return View();
        }

        public IActionResult DeleteRole(string roleName)
        {
            return View();
        }

    }
}