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
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Start
{
    public class IdentitySeedData
    {
        public static async void PopulateIdentity(IApplicationBuilder app)
        {
            UserManager<SportUser> userManger = app.ApplicationServices.GetRequiredService<UserManager<SportUser>>();
            RoleManager<IdentityRole> roleManger = app.ApplicationServices.GetRequiredService<RoleManager<IdentityRole>>();
            IPermissionRepository permissionRepository = app.ApplicationServices.GetRequiredService<IPermissionRepository>();

            var adminRole = roleManger.Roles.FirstOrDefault(r => r.Name == IdentityRoleNames.Admins);
            var adminUser = userManger.Users.FirstOrDefault(u => u.UserName == "admin");

            if(adminRole == null)
            {
                adminRole = new IdentityRole(IdentityRoleNames.Admins);
                await roleManger.CreateAsync(adminRole);
            }

            if (adminUser == null)
            {
                adminUser = new SportUser("admin");
                await userManger.CreateAsync(adminUser, "Admin1!");
            }

            if(!await userManger.IsInRoleAsync(adminUser, adminRole.Name))
            {
                await userManger.AddToRoleAsync(adminUser, adminRole.Name);
            }

            foreach(Permission permission in permissionRepository.Permissions.Where(p => p.Category == SecurityPermissionCategories.Security))
            {
                var adminRoleClaims = await roleManger.GetClaimsAsync(adminRole);
                var adminUserClaims = await userManger.GetClaimsAsync(adminUser);

                if(!adminRoleClaims.Any(c => c.Value == permission.Value))
                {
                    Claim newClaim = new Claim(ClaimTypes.AuthenticationMethod, permission.Value, ClaimValueTypes.String);
                    await roleManger.AddClaimAsync(adminRole, newClaim);
                }

                if(!adminUserClaims.Any(c => c.Value == permission.Value))
                {
                    Claim newClaim = new Claim(ClaimTypes.AuthenticationMethod, permission.Value, ClaimValueTypes.String);
                    await userManger.AddClaimAsync(adminUser, newClaim);
                }
            }
        }
    }
}
