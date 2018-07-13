using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Domain;
using SportsStore.Infrastructure.Extensions;
using SportsStore.Models.Identity;
using SportsStore.Models.Role;

namespace SportsStore.Controllers
{
    [Authorize]
    public class RoleController : BaseController
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<SportUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<SportUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [Authorize(Policy = SecurityPermissionValues.ViewRole)]
        public IActionResult List()
        {
            return View(_roleManager.Roles.ToList());
        }

        [Authorize(Policy = SecurityPermissionValues.AddRole)]
        public IActionResult CreateRole()
        {
            return View(new CreateRoleViewModel());
        }

        [HttpPost]
        [Authorize(Policy = SecurityPermissionValues.AddRole)]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(model.Role);
                if (result.Succeeded)
                {
                    foreach(var permission in model.SelectedPermission)
                    {
                        Claim claim = new Claim(ClaimTypes.AuthenticationMethod, permission);
                        await _roleManager.AddClaimAsync(model.Role, claim);
                    }
                    return RedirectToAction("List");
                }
                else
                    result.Errors.ToList().ForEach(e => { ModelState.AddModelError("", e.Description); });
            }
            return View();
        }

        [Authorize(Policy = SecurityPermissionValues.EditRole)]
        public async Task<IActionResult> EditRole(string roleName)
        {
            List<SportUser> usersInRole = new List<SportUser>();
            foreach(var user in _userManager.Users)
            {
                if (roleName != IdentityRoleNames.Clients && await _userManager.IsInRoleAsync(user, roleName))
                    usersInRole.Add(user);
            }

            EditRoleViewModel model = new EditRoleViewModel { Role = await _roleManager.FindByNameAsync(roleName), UsersInRole = usersInRole };
            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = SecurityPermissionValues.EditRole)]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole roleToUpdate = await _roleManager.FindByNameAsync(model.Role.Name);
                if (roleToUpdate != null)
                {
                    bool finalSuccess = true;

                    var roleClaims = await _roleManager.GetClaimsAsync(roleToUpdate);
                    var claimsSortBySelection = SortClaimsBySelection(model.SelectedPermission, roleClaims);
                    var selectedPermissions = claimsSortBySelection.Item1;
                    var unselectedPermissions = claimsSortBySelection.Item2;

                    var roleUsers = await _userManager.GetUsersInRoleAsync(roleToUpdate.Name);

                    foreach (string claim in selectedPermissions)
                    {
                        if (roleClaims.Any(c => c.Value == claim))
                            continue;

                        var newClaim = new Claim(ClaimTypes.AuthenticationMethod, claim, ClaimValueTypes.Boolean, "SPORTSSTORE");
                        var addClaimResult = await _roleManager.AddClaimAsync(roleToUpdate, newClaim);

                        if (addClaimResult.Errors.Any())
                        {
                            AddErrors(addClaimResult.Errors);
                            finalSuccess = false;
                        }

                        foreach(var user in roleUsers)
                        {
                            var addClaimToUserResult = await _userManager.AddClaimAsync(user, newClaim);

                            if (addClaimToUserResult.Errors.Any())
                            {
                                AddErrors(addClaimToUserResult.Errors);
                                finalSuccess = false;
                            }
                        }
                    }

                    foreach(string claim in unselectedPermissions)
                    {
                        var claimToRemove = roleClaims.FirstOrDefault(c => c.Value == claim);
                        if(claimToRemove != null)
                        {
                            var removeClaimResult = await _roleManager.RemoveClaimAsync(roleToUpdate, claimToRemove);

                            if(removeClaimResult.Errors.Any())
                            {
                                AddErrors(removeClaimResult.Errors);
                                finalSuccess = false;
                            }
                        }

                        foreach(var user in roleUsers)
                        {
                            var removeClaimFromUserResult = await _userManager.RemoveClaimAsync(user, claimToRemove);
                            if (removeClaimFromUserResult.Errors.Any())
                            {
                                AddErrors(removeClaimFromUserResult.Errors);
                                finalSuccess = false;
                            }
                        }
                    }

                    if (finalSuccess)
                        return RedirectToAction("List");
                }
            }

            return View(model);
        }

        [Authorize(Policy = SecurityPermissionValues.DeleteRole)]
        public IActionResult DeleteRole(string roleName)
        {
            return View();
        }

        public async Task<IActionResult> AddToRole(EditRoleViewModel model)
        {
            SportUser user = null;
            if (model.SearchedUserData.Contains("@"))
                user = await _userManager.FindByEmailAsync(model.SearchedUserData);
            else
                user = await _userManager.FindByNameAsync(model.SearchedUserData);

            if (user == null)
                ModelState.AddModelError("", "Nie znaleziono użytkownika");

            var addToRoleResult = await _userManager.AddToRoleAsync(user, model.Role.Name);

            if(addToRoleResult.Errors.Any())
                AddErrors(addToRoleResult.Errors);

            var roleClaims = await _roleManager.GetClaimsAsync(model.Role);
            foreach(var roleClaim in roleClaims)
            {
                var addClaimResult = await _userManager.AddClaimAsync(user, roleClaim);

                if(addClaimResult.Errors.Any())
                    AddErrors(addClaimResult.Errors);
            }

            return RedirectToAction("EditRole","Role", new { roleName = model.Role.Name });
        }

        [Authorize(Policy = SecurityPermissionValues.EditUser)]
        public async Task<IActionResult> DeleteFromRole(string roleName, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var role = await _roleManager.FindByNameAsync(roleName);

            var claims = await _roleManager.GetClaimsAsync(role);
            foreach(var claim in claims)
            {
                Claim claimToRemove = new Claim(ClaimTypes.AuthenticationMethod, claim.Value);
                await _userManager.RemoveClaimAsync(user, claimToRemove);
            }

            await _userManager.RemoveFromRoleAsync(user, roleName);

            return RedirectToAction("EditRole", "Role", new { roleName = roleName });
        }

        private  Tuple<List<string>, List<string>> SortClaimsBySelection(List<string> selectedClaims, IList<Claim> claims)
        {
            List<string> unselectedClaims = new List<string>();

            if (selectedClaims == null)
                return new Tuple<List<string>, List<string>>(new List<string>(), claims.Select(c => c.Value).ToList());

            foreach (var claim in claims)
            {
                if (!selectedClaims.Contains(claim.Value))
                    unselectedClaims.Add(claim.Value);
            }

            return new Tuple<List<string>, List<string>>(selectedClaims, unselectedClaims);
        }
    }
}