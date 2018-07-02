using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCaching.Internal;
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

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userRoles = await _userManager.GetRolesAsync(user);
            return View(new UserIndexViewModel{User = user, UserRoles = userRoles });
        }

        public async Task<IActionResult> List(string roleName = "Wszystkie")
        {
            var avaibleRoles = _roleManager.Roles.Select(r => r.Name).ToList();
            avaibleRoles.Add("Wszystkie");
            UserListViewModel model = new UserListViewModel{ Roles =  avaibleRoles };

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
                if (createResult.Errors.Any())
                {
                    AddErrors(createResult.Errors);
                    finalSuccess = false;
                }

                if (!User.Identity.IsAuthenticated)
                    await _userManager.AddToRoleAsync(model.User, IdentityRoles.Clients);

                if(model.SelectedRoles != null)
                {
                    foreach (string roleId in model.SelectedRoles)
                    {
                        var addRoleResult = await _userManager.AddToRoleAsync(model.User, roleId);
                        if (addRoleResult.Errors.Any())
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

        public async Task<IActionResult> EditUser(string userId)
        {          
            var user = await _userManager.FindByIdAsync(userId);
            EditUserViewModel model = new EditUserViewModel{User = user};
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = model.User;
            var splitedRolesBySelection = SortRolesBySelection(model.SelectedRoles);
            var rolesToAdd = splitedRolesBySelection.Item1;
            var rolesToDelete = splitedRolesBySelection.Item2;
            bool finalSuccess = true;

            if (ModelState.IsValid)
            {
                var userToUpdate = await _userManager.FindByNameAsync(user.UserName);

                userToUpdate.UserName = user.UserName;
                userToUpdate.Email = user.Email;

                var result = await _userManager.UpdateAsync(userToUpdate);
                if (result.Errors.Any())
                {
                    AddErrors(result.Errors);
                    finalSuccess = false;
                }

                foreach (string roleToAdd in rolesToAdd)
                {
                    if (!await _userManager.IsInRoleAsync(userToUpdate, roleToAdd))
                    {
                        var addResult = await _userManager.AddToRoleAsync(userToUpdate, roleToAdd);
                        if (addResult.Errors.Any())
                        {
                            AddErrors(addResult.Errors);
                            finalSuccess = false;
                        }
                    }
                }

                foreach (string roleToDelete in rolesToDelete)
                {
                    if (await _userManager.IsInRoleAsync(userToUpdate, roleToDelete))
                    {
                        var deleteResult = await _userManager.RemoveFromRoleAsync(userToUpdate, roleToDelete);
                        if (deleteResult.Errors.Any())
                        {
                            AddErrors(deleteResult.Errors);
                            finalSuccess = false;
                        }
                    }
                    
                }

                if (finalSuccess)
                    return RedirectToAction("List");
            }

            return View(model);
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

        private Tuple<IEnumerable<string>, IEnumerable<string>> SortRolesBySelection(IEnumerable<string> selectedRoles)
        {
            if(selectedRoles == null)
                return new Tuple<IEnumerable<string>, IEnumerable<string>>(new List<string>(), _roleManager.Roles.Select(r => r.Name));

            var unselectedRoles = new List<string>();
            foreach (var role in _roleManager.Roles)
            {
                if(!selectedRoles.Contains(role.Name))
                    unselectedRoles.Add(role.Name);
            }

            return  new Tuple<IEnumerable<string>, IEnumerable<string>>(selectedRoles, unselectedRoles);
        }
    }
}
