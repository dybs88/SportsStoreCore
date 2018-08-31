using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Controllers.Base
{
    public class IdentityController : BaseController
    {
        public IdentityController(IServiceProvider provider)
            :base(provider)
        { }
        public void AddIdentityClaim(IEnumerable<Claim> claims)
        {
            User.AddIdentity(new ClaimsIdentity(claims));
        }
    }
}
