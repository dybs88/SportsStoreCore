using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportsStore.DAL.Repos.Security;
using SportsStore.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Components
{
    public class PermissionTableViewComponent : ViewComponent
    {
        private RoleManager<IdentityRole> _roleManager;
        private IPermissionRepository _permissionRepository;

        public PermissionTableViewComponent(RoleManager<IdentityRole> roleManager, IPermissionRepository repo)
        {
            _roleManager = roleManager;
            _permissionRepository = repo;
        }

        public IViewComponentResult Invoke(IdentityRole role)
        {
            IList<Claim> roleClaims = new List<Claim>();
            if(role != null)
                roleClaims = _roleManager.GetClaimsAsync(role).Result;

            Dictionary<Permission, bool> model = new Dictionary<Permission, bool>();

            foreach(var permission in _permissionRepository.Permissions)
            {
                bool isRoleHasClaim = roleClaims.Any(c => c.Value == permission.Value);
                model.Add(permission, isRoleHasClaim);
            }

            return View(model);
        }
    }
}
