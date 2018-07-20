using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.DAL.Repos.CustomerSchema;
using SportsStore.Domain;
using SportsStore.Models.DAL.Repos.SalesSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Policies
{
    public class BasePolicies
    {
        protected static Func<AuthorizationHandlerContext, bool> IsCustomerOwnerHandler = IsCustomerOwnerMethod;
        protected static Func<AuthorizationHandlerContext, bool> IsAddressOwnerHandler = IsAdddressOwnerMethod;
        protected static Func<AuthorizationHandlerContext, bool> IsCurrentUserHandler = IsCurrentUserMethod;
        protected static Func<AuthorizationHandlerContext, bool> IsOrderOwnerHandler = IsOrderOwnerMethod;

        static bool IsOrderOwnerMethod(AuthorizationHandlerContext context)
        {
            Claim claim = null;
            if (context.User.IsInRole(IdentityRoleNames.Employees))
            {
                claim = context.User.Claims.FirstOrDefault(c => c.Value == SalesPermissionValues.ViewOrder);
                if (claim == null)
                    return false;
                else
                    return true;
            }
            else
            {
                AuthorizationFilterContext filterContext = (AuthorizationFilterContext)context.Resource;
                IOrderRepository orderRepository = filterContext.HttpContext.RequestServices.GetRequiredService<IOrderRepository>();

                var customerId = filterContext.RouteData.Values["customerId"].ToString();
                var orderId = filterContext.RouteData.Values["orderId"].ToString();

                return orderRepository.CheckIfCustomerIsOrderOwner(int.Parse(customerId), int.Parse(orderId));
            }
        }

        static bool IsCustomerOwnerMethod(AuthorizationHandlerContext context)
        {
            Claim claim = null;
            if(context.User.IsInRole(IdentityRoleNames.Employees))
            {
                claim = context.User.Claims.FirstOrDefault(c => c.Value == CustomerPermissionValues.ViewCustomer);
                if (claim == null)
                    return false;
                else
                    return true;
            }
            else
            {
                claim = context.User.Claims.FirstOrDefault(c => c.Type == SportsStoreClaimTypes.CustomerId);
                if (claim == null)
                    return false;

                AuthorizationFilterContext filterContext = (AuthorizationFilterContext)context.Resource;
                var customerId = filterContext.RouteData.Values["customerId"].ToString();
                if (claim.Value == customerId)
                    return true;
            }
           
            return false;
        }

        static bool IsAdddressOwnerMethod(AuthorizationHandlerContext context)
        {
            Claim claim = null;
            if (context.User.IsInRole(IdentityRoleNames.Employees))
            {
                claim = context.User.Claims.FirstOrDefault(c => c.Value == CustomerPermissionValues.ViewAddress);
                if (claim == null)
                    return false;
                else
                    return true;
            }
            else
            {
                AuthorizationFilterContext filterContext = (AuthorizationFilterContext)context.Resource;
                IAddressRepository addressRepository = filterContext.HttpContext.RequestServices.GetRequiredService<IAddressRepository>();

                var customerId = filterContext.RouteData.Values["customerId"].ToString();
                var addressId = filterContext.RouteData.Values["addressId"].ToString();

                return addressRepository.CheckIfCustomerIsAddressOwner(int.Parse(customerId), int.Parse(addressId));
            }
        }

        static bool IsCurrentUserMethod(AuthorizationHandlerContext context)
        {
            if (context.User.IsInRole(IdentityRoleNames.Admins))
                return true;

            var claim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
            if (claim == null)
                return false;

            if (context.User.Identity.Name == claim.Value)
                return true;

            return false;
        }
    }
}
