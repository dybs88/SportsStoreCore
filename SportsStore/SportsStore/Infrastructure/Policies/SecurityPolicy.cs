using Microsoft.Extensions.DependencyInjection;
using SportsStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Policies
{
    public static class SecurityPolicy
    {
        public static void AddSecurityPolicies(IServiceCollection services)
        {
            services.AddAuthorization(o =>
            {
                o.AddPolicy(SecurityPermssionValues.EditUser, policy => 
                    {
                        policy.RequireRole(IdentityRoleNames.Admins);
                        policy.RequireClaim(ClaimTypes.AuthenticationMethod, SecurityPermssionValues.EditUser);
                    });
                o.AddPolicy(SecurityPermssionValues.DeleteUser, policy =>
                    {
                        policy.RequireRole(IdentityRoleNames.Admins);
                        policy.RequireClaim(ClaimTypes.AuthenticationMethod, SecurityPermssionValues.DeleteUser);
                    });

                o.AddPolicy(SecurityPermssionValues.AddRole, policy =>
                {
                    policy.RequireRole(IdentityRoleNames.Admins);
                    policy.RequireClaim(ClaimTypes.AuthenticationMethod, SecurityPermssionValues.AddRole);
                });
                o.AddPolicy(SecurityPermssionValues.EditRole, policy =>
                {
                    policy.RequireRole(IdentityRoleNames.Admins);
                    policy.RequireClaim(ClaimTypes.AuthenticationMethod, SecurityPermssionValues.EditRole);
                });

                o.AddPolicy(SecurityPermssionValues.DeleteRole, policy =>
                {
                    policy.RequireRole(IdentityRoleNames.Admins);
                    policy.RequireClaim(ClaimTypes.AuthenticationMethod, SecurityPermssionValues.DeleteRole);
                });
            });
        }
    }
}
