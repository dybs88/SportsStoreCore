using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.DAL.Contexts;
using SportsStore.DAL.Repos.Security;
using SportsStore.Domain;
using SportsStore.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Start
{
    public class IdentitySeedData
    {
        public static async void PopulateIdentity(IApplicationBuilder app)
        {
            UserManager<SportUser> userManager = app.ApplicationServices.GetRequiredService<UserManager<SportUser>>();
            RoleManager<IdentityRole> roleManager = app.ApplicationServices.GetRequiredService<RoleManager<IdentityRole>>();
            IPermissionRepository permissionRepository = app.ApplicationServices.GetRequiredService<IPermissionRepository>();

            var adminRole = roleManager.Roles.FirstOrDefault(r => r.Name == IdentityRoleNames.Admins);
            var adminUser = userManager.Users.FirstOrDefault(u => u.UserName == "admin");

            if(adminRole == null)
            {
                adminRole = new IdentityRole(IdentityRoleNames.Admins);
                await roleManager.CreateAsync(adminRole);
            }

            if (adminUser == null)
            {
                adminUser = new SportUser("admin");
                await userManager.CreateAsync(adminUser, "Admin1!");
            }

            if(!await userManager.IsInRoleAsync(adminUser, adminRole.Name))
            {
                await userManager.AddToRoleAsync(adminUser, adminRole.Name);
            }

            var adminRoleClaims = await roleManager.GetClaimsAsync(adminRole);
            var adminUserClaims = await userManager.GetClaimsAsync(adminUser);

            var securityPermissions = permissionRepository.Permissions.Where(p => p.Category == SecurityPermissionCategories.Security);
            foreach(Permission permission in securityPermissions)
            {
                if(!adminRoleClaims.Any(c => c.Value == permission.Value))
                {
                    Claim newClaim = new Claim(ClaimTypes.AuthenticationMethod, permission.Value, ClaimValueTypes.String);
                    await roleManager.AddClaimAsync(adminRole, newClaim);
                }

                if(!adminUserClaims.Any(c => c.Value == permission.Value))
                {
                    Claim newClaim = new Claim(ClaimTypes.AuthenticationMethod, permission.Value, ClaimValueTypes.String);
                    await userManager.AddClaimAsync(adminUser, newClaim);
                }
            }
        }
    }
}
