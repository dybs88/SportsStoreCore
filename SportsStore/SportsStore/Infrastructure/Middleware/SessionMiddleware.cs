using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.DAL.Repos.Security;
using SportsStore.Domain;
using SportsStore.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Middleware
{
    public class SessionMiddleware
    {
        private RequestDelegate _next;

        public SessionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ISportsStoreUserManager userManager)
        {
            if(context.User.Identity.IsAuthenticated && !context.Session.Keys.Contains(SessionData.CustomerId))
            {
                var user = await userManager.FindByNameAsync(context.User.Identity.Name);
                context.Session.SetString(SessionData.CustomerId, user.CustomerId.ToString());
            }
            
            await _next.Invoke(context);
        }
    }
}
