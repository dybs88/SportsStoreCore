using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
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
    public class FakeSignInManager : SignInManager<SportUser>
    {
        public FakeSignInManager()
            : base(new Mock<FakeUserManager>().Object, 
                   new HttpContextAccessor(), 
                   new Mock<IUserClaimsPrincipalFactory<SportUser>>().Object, 
                   new Mock<IOptions<IdentityOptions>>().Object,
                   new Mock<ILogger<SignInManager<SportUser>>>().Object,
                   new Mock<IAuthenticationSchemeProvider>().Object)
        { }                
    }
}
