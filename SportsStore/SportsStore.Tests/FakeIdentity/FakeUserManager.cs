using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using SportsStore.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportsStore.Tests.FakeIdentity
{
    public class FakeUserManager : UserManager<SportUser>
    {
        public FakeUserManager(
            IUserStore<SportUser> store, 
            IOptions<IdentityOptions> options, 
            IPasswordHasher<SportUser> passwordHasher, 
            IUserValidator<SportUser> userValidator,
            IPasswordValidator<SportUser> passwordValidator,
            ILookupNormalizer normalizer,
            IdentityErrorDescriber errorDescriber,
            IServiceProvider serviceProvider,
            ILogger<FakeUserManager> logger)
        : base (store, options, passwordHasher, new[] { userValidator }, new[] { passwordValidator}, normalizer, errorDescriber, serviceProvider, logger)
        { }

        public FakeUserManager()
            :base (new Mock<IUserStore<SportUser>>().Object,
                  new Mock<IOptions<IdentityOptions>>().Object,
                  new Mock<IPasswordHasher<SportUser>>().Object,
                  new IUserValidator<SportUser>[0],
                  new IPasswordValidator<SportUser>[0],
                  new Mock<ILookupNormalizer>().Object,
                  new Mock<IdentityErrorDescriber>().Object,
                  new Mock<IServiceProvider>().Object,
                  new Mock<ILogger<UserManager<SportUser>>>().Object)
        { }
    }
}
