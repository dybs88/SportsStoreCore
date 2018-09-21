using Microsoft.AspNetCore.Identity;
using SportsStore.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.DAL.Repos.Security
{
    public interface ISportsStoreUserManager
    {
        IQueryable<SportUser> Users { get; }
        Task<SportUser> FindByNameAsync(string name);
        Task<SportUser> FindByEmailAsync(string email);
        Task<IdentityResult> UpdateAsync(SportUser user); 
    }
}
