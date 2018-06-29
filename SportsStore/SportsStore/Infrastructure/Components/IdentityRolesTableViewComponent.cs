using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Components
{
    public class IdentityRolesTableViewComponent : ViewComponent
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<IdentityUser> _userManager;

        public IdentityRolesTableViewComponent(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IViewComponentResult Invoke(IdentityUser user)
        {
            Dictionary<IdentityRole, bool> componentModel = new Dictionary<IdentityRole, bool>();
            foreach(var role in _roleManager.Roles)
            {
                componentModel.Add(role, _userManager.IsInRoleAsync(user, role.Name).Result);
            }

            return View(componentModel);
        }
    }
}
