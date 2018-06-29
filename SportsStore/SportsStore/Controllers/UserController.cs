using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Domain;
using SportsStore.Models;
using SportsStore.Models.User;

namespace SportsStore.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var user = _userManager.GetUserAsync(HttpContext.User);
            return View(user.Result);
        }

        public async Task<IActionResult> List()
        {
            UserListViewModel model = new UserListViewModel();
            
            foreach(var user in _userManager.Users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                model.UsersWithRoles.Add(user, userRoles);
            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult CreateUser(string returnUrl)
        {
            return View(new CreateUserViewModel { User = new IdentityUser(), AvaibleRoles = _roleManager.Roles, ReturnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool finalSuccess = true;
                var createResult = await _userManager.CreateAsync(model.User, model.Password);
                if (createResult.Errors.Count() > 0)
                {
                    AddErrors(createResult.Errors);
                    finalSuccess = false;
                }

                if (!User.Identity.IsAuthenticated)
                    await _userManager.AddToRoleAsync(model.User, IdentityRoles.Clients);

                if(model.AddedRolesIds != null)
                {
                    foreach (string roleId in model.AddedRolesIds)
                    {
                        var addRoleResult = await _userManager.AddToRoleAsync(model.User, roleId);
                        if (addRoleResult.Errors.Count() > 0)
                        {
                            AddErrors(addRoleResult.Errors);
                            finalSuccess = false;
                        }
                    }
                }


                if (finalSuccess)
                    return Redirect(model.ReturnUrl);
            }

            return View("CreateUser", model);        
        }

        public IActionResult EditUser(string userId)
        {
            var user = _userManager.FindByIdAsync(userId);
            return View(user.Result);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(IdentityUser user)
        {
            if (ModelState.IsValid)
            {
                var userToUpdate = await _userManager.FindByNameAsync(user.UserName);

                userToUpdate.UserName = user.UserName;
                userToUpdate.Email = user.Email;

                //await _userManager.UpdateSecurityStampAsync(userToUpdate);
                var result = await _userManager.UpdateAsync(userToUpdate);
                return RedirectToAction("List");
            }
            else
                return View(user);
        }

        public async Task<IActionResult> DeleteUser(string userId)
        {
            var userToDelete = await _userManager.FindByIdAsync(userId);

            if(userToDelete != null)
            {
                var deleteResult = await _userManager.DeleteAsync(userToDelete);
                if (!deleteResult.Succeeded)
                    AddErrors(deleteResult.Errors);
            }

            return RedirectToAction("List");
        }
    }
}
