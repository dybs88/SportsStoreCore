using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace SportsStore.Controllers.Base
{
    public class IdentityController : BaseController
    {
        public IdentityController(IServiceProvider provider, IConfiguration config)
            :base(provider, config)
        { }
        public void AddIdentityClaim(IEnumerable<Claim> claims)
        {
            User.AddIdentity(new ClaimsIdentity(claims));
        }
    }
}
