using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.DAL.Repos.CustomerSchema;
using SportsStore.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<bool> HasClaimsAsync<TUser>(this UserManager<TUser> manager, TUser user, string claimValue) where TUser : SportUser
        {
            var userClaims = await manager.GetClaimsAsync(user);
            return userClaims.Any(c => c.Value == claimValue);
        }

        public static async Task<TUser> FindByCustomerIdAsync<TUser>(this UserManager<TUser> manager, int customerId) where TUser : SportUser
        {
            return manager.Users.FirstOrDefault(u => u.CustomerId == customerId);
        }

        public static async Task<int> GetCustomerIdByNameAsync<TUser>(this UserManager<SportUser> manager, string userName) where TUser: SportUser
        {
            return manager.Users.FirstOrDefault(u => u.UserName == userName)?.CustomerId ?? 0;
        }
    }
}
