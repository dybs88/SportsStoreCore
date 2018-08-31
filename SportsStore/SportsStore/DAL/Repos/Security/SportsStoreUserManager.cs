using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SportsStore.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.DAL.Repos.Security
{
    public class SportsStoreUserManager : UserManager<SportUser>, ISportsStoreUserManager
    {
        public SportsStoreUserManager(IUserStore<SportUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<SportUser> passwordHasher, IEnumerable<IUserValidator<SportUser>> userValidators, IEnumerable<IPasswordValidator<SportUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<SportUser>> logger) 
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }
    }
}
