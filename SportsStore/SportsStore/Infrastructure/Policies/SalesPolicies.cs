using Microsoft.Extensions.DependencyInjection;
using SportsStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Policies
{
    public class SalesPolicies : BasePolicies
    {
        public static void AddSalesPolicies(IServiceCollection services)
        {
            services.AddAuthorization(o => 
            {
                o.AddPolicy(SalesPermissionValues.ViewOrder, policy => 
                {
                    policy.RequireClaim(ClaimTypes.AuthenticationMethod, SalesPermissionValues.ViewOrder);
                    policy.RequireAssertion(IsCustomerOwnerHandler);
                });

                o.AddPolicy(SalesPermissionValues.AddOrder, policy =>
                {
                    policy.RequireClaim(ClaimTypes.AuthenticationMethod, SalesPermissionValues.AddOrder);
                });

                o.AddPolicy(SalesPermissionValues.EditOrder, policy =>
                {
                    policy.RequireClaim(ClaimTypes.AuthenticationMethod, SalesPermissionValues.EditOrder);
                    policy.RequireAssertion(IsOrderOwnerHandler);
                });

                o.AddPolicy(SalesPermissionValues.DeleteOrder, policy =>
                {
                    policy.RequireClaim(ClaimTypes.AuthenticationMethod, SalesPermissionValues.DeleteOrder);
                });
            });
        }
    }
}
