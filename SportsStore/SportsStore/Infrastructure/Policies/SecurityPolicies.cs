using Microsoft.Extensions.DependencyInjection;
using SportsStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Policies
{
    public static class SecurityPolicies
    {
        public static void AddSecurityPolicies(IServiceCollection services)
        {
            services.AddAuthorization(o =>
            {
                o.AddPolicy(SecurityPermissionValues.ViewUser, policy =>
                {
                    policy.RequireClaim(ClaimTypes.AuthenticationMethod, SecurityPermissionValues.ViewUser);
                });
                o.AddPolicy(SecurityPermissionValues.EditUser, policy => 
                {
                    policy.RequireRole(IdentityRoleNames.Admins);
                    policy.RequireClaim(ClaimTypes.AuthenticationMethod, SecurityPermissionValues.EditUser);
                });
                o.AddPolicy(SecurityPermissionValues.DeleteUser, policy =>
                {
                    policy.RequireRole(IdentityRoleNames.Admins);
                    policy.RequireClaim(ClaimTypes.AuthenticationMethod, SecurityPermissionValues.DeleteUser);
                });

                o.AddPolicy(SecurityPermissionValues.ViewRole, policy => 
                {
                    policy.RequireRole(IdentityRoleNames.Admins);
                    policy.RequireClaim(ClaimTypes.AuthenticationMethod, SecurityPermissionValues.ViewRole);
                });
                o.AddPolicy(SecurityPermissionValues.AddRole, policy =>
                {
                    policy.RequireRole(IdentityRoleNames.Admins);
                    policy.RequireClaim(ClaimTypes.AuthenticationMethod, SecurityPermissionValues.AddRole);                   
                });
                o.AddPolicy(SecurityPermissionValues.EditRole, policy =>
                {
                    policy.RequireRole(IdentityRoleNames.Admins);
                    policy.RequireClaim(ClaimTypes.AuthenticationMethod, SecurityPermissionValues.EditRole);
                });

                o.AddPolicy(SecurityPermissionValues.DeleteRole, policy =>
                {
                    policy.RequireRole(IdentityRoleNames.Admins);
                    policy.RequireClaim(ClaimTypes.AuthenticationMethod, SecurityPermissionValues.DeleteRole);
                });
            });
        }
    }
}
