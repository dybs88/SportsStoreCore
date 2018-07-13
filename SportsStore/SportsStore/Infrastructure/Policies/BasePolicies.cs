using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using SportsStore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Policies
{
    public class BasePolicies
    {
        protected static Func<AuthorizationHandlerContext, bool> IsOwnerHandler = IsOwnerMethod;

        static bool IsOwnerMethod(AuthorizationHandlerContext context)
        {
            var claim = context.User.Claims.FirstOrDefault(c => c.Type == SportsStoreClaimTypes.CustomerId);
            if (claim == null)
                return false;

            AuthorizationFilterContext filterContext = (AuthorizationFilterContext)context.Resource;
            var customerId = filterContext.RouteData.Values["customerId"].ToString();
            if (claim.Value == customerId)
                return true;

            return false;
        }
    }
}
