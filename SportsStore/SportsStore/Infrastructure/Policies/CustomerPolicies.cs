using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Policies
{
    public class CustomerPolicies : BasePolicies
    {
        public static void AddCustomerPolicies(IServiceCollection services)
        {
            services.AddAuthorization(o => 
            {
                o.AddPolicy(CustomerPermissionValues.ViewCustomer, policy => 
                {
                    policy.RequireClaim(ClaimTypes.AuthenticationMethod, CustomerPermissionValues.ViewCustomer);
                    policy.RequireAssertion(IsOwnerHandler);
                });
                o.AddPolicy(CustomerPermissionValues.AddCustomer, policy =>
                {
                    policy.RequireClaim(ClaimTypes.AuthenticationMethod, CustomerPermissionValues.AddCustomer);
                });
                o.AddPolicy(CustomerPermissionValues.EditCustomer, policy =>
                {
                    policy.RequireClaim(ClaimTypes.AuthenticationMethod, CustomerPermissionValues.EditCustomer);
                    policy.RequireAssertion(IsOwnerHandler);

                });
                o.AddPolicy(CustomerPermissionValues.DeleteCustomer, policy =>
                {
                    policy.RequireClaim(ClaimTypes.AuthenticationMethod, CustomerPermissionValues.DeleteCustomer);
                });

                o.AddPolicy(CustomerPermissionValues.ViewAddress, policy => 
                {
                    policy.RequireClaim(ClaimTypes.AuthenticationMethod, CustomerPermissionValues.ViewAddress);
                    policy.RequireAssertion(IsOwnerHandler);
                });

                o.AddPolicy(CustomerPermissionValues.AddAddress, policy =>
                {
                    policy.RequireClaim(ClaimTypes.AuthenticationMethod, CustomerPermissionValues.AddAddress);
                    policy.RequireAssertion(IsOwnerHandler);
                });
                o.AddPolicy(CustomerPermissionValues.EditAddress, policy =>
                {
                    policy.RequireClaim(ClaimTypes.AuthenticationMethod, CustomerPermissionValues.EditAddress);
                    policy.RequireAssertion(IsOwnerHandler);
                });
                o.AddPolicy(CustomerPermissionValues.DeleteAddress, policy =>
                {
                    policy.RequireClaim(ClaimTypes.AuthenticationMethod, CustomerPermissionValues.DeleteAddress);
                    policy.RequireAssertion(IsOwnerHandler);
                });
            });
        }
    }
}
