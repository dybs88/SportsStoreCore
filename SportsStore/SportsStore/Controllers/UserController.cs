using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCaching.Internal;
using SportsStore.Controllers.Base;
using SportsStore.DAL.Repos.CustomerSchema;
using SportsStore.Domain;
using SportsStore.Models.CustomerModels;
using SportsStore.Models.Identity;
using SportsStore.Models.User;

namespace SportsStore.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private UserManager<SportUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ICustomerRepository _customerRepository;

        public UserController(UserManager<SportUser> userManager, RoleManager<IdentityRole> roleManager, ICustomerRepository customerRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _customerRepository = customerRepository;
        }

        [Authorize(Policy = SecurityPermissionValues.ViewUser)]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userRoles = await _userManager.GetRolesAsync(user);
            return View(new UserIndexViewModel{User = user, UserRoles = userRoles });
        }

        [Authorize(Roles = IdentityRoleNames.Admins)]
        [Authorize(Policy = SecurityPermissionValues.ViewUser)]
        public async Task<IActionResult> List(string roleName)
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
            return View(new CreateUserViewModel
                        {
                            User = new SportUser(),
                            AvaibleRoles = _roleManager.Roles,
                            ReturnUrl = returnUrl,
                            IsAuthenticated = User.Identity.IsAuthenticated
                        });
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                int customerId = 0;
                if (!model.IsAuthenticated)
                {
                    Customer newCustomer = new Customer(model.User.FirstName, model.User.LastName);
                    newCustomer.Email = model.User.Email;
                    customerId = _customerRepository.SaveCustomer(newCustomer);
                }

                bool finalSuccess = true;
                model.User.CustomerId = customerId;
                var createResult = await _userManager.CreateAsync(model.User, model.Password);
                if (createResult.Errors.Any())
                {
                    AddErrors(createResult.Errors);
                    finalSuccess = false;
                }

                Claim idClaim = new Claim(SportsStoreClaimTypes.CustomerId, customerId.ToString(), ClaimValueTypes.Integer32);
                await _userManager.AddClaimAsync(model.User, idClaim);

                if (!model.IsAuthenticated)
                {
                    await _userManager.AddToRoleAsync(model.User, IdentityRoleNames.Clients);
                    var roleClaims = await _roleManager.GetClaimsAsync(await _roleManager.FindByNameAsync(IdentityRoleNames.Clients));

                    foreach(var claim in roleClaims)
                    {
                        await _userManager.AddClaimAsync(model.User, claim);
                    }

                }
                else
                {
                    if (model.SelectedRoles != null)
                    {
                        foreach (string roleName in model.SelectedRoles)
                        {
                            var addRoleResult = await _userManager.AddToRoleAsync(model.User, roleName);
                            if (addRoleResult.Errors.Any())
                            {
                                AddErrors(addRoleResult.Errors);
                                finalSuccess = false;
                            }

                            var roleClaims = await _roleManager.GetClaimsAsync(await _roleManager.FindByNameAsync(roleName));

                            foreach (var claim in roleClaims)
                            {
                                await _userManager.AddClaimAsync(model.User, claim);
                            }
                        }
                    }
                }

                if (finalSuccess)
                    return Redirect(model.ReturnUrl);
            }

            return View("CreateUser", model);        
        }
        [Authorize(Policy = SecurityPermissionValues.EditUser)]
        public async Task<IActionResult> EditUser(string userId)
        {          
            var user = await _userManager.FindByIdAsync(userId);
            EditUserViewModel model = new EditUserViewModel{User = user};
            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = SecurityPermissionValues.EditUser)]
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
                userToUpdate.FirstName = user.FirstName;
                userToUpdate.LastName = user.LastName;

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

                if(await _userManager.IsInRoleAsync(user, IdentityRoleNames.Clients))
                {
                    var customer = _customerRepository.GetCustomer(user.CustomerId);
                    customer.FirstName = user.FirstName;
                    customer.LastName = user.LastName;
                    customer.Email = user.Email;

                    _customerRepository.SaveCustomer(customer);
                }

                if (finalSuccess)
                    return RedirectToAction("List");
            }

            return View(model);
        }

        [Authorize(Policy = SecurityPermissionValues.DeleteUser)]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var userToDelete = await _userManager.FindByIdAsync(userId);
            if(userToDelete.CustomerId != 0)
                _customerRepository.DeleteCustomer(userToDelete.CustomerId);

            if(userToDelete != null)
            {
                var deleteResult = await _userManager.DeleteAsync(userToDelete);
                if (!deleteResult.Succeeded)
                    AddErrors(deleteResult.Errors);
            }

            return RedirectToAction("List");
        }

        [Authorize(Policy = SecurityPermissionValues.EditUser)]
        public IActionResult ResetPassword(string userId)
        {
            return View(new ResetPasswordViewModel { UserId = userId });
        }

        [HttpPost]
        [Authorize(Policy = SecurityPermissionValues.EditUser)]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if(user != null)
                {
                    var hashedPassword = _userManager.PasswordHasher.HashPassword(user, model.Password);
                    var resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var updateResult = await _userManager.ResetPasswordAsync(user, resetPasswordToken, model.Password);

                    if (updateResult.Errors.Any())
                    {
                        AddErrors(updateResult.Errors);
                        return View(model.UserId);
                    }
                }

                return RedirectToAction("List");
            }

            return View(model.UserId);
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
