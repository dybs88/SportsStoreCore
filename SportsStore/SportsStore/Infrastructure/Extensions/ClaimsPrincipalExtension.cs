using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static bool HasClaim(this ClaimsPrincipal user, string claimType, string claimValue)
        {
            return user.Claims.Any(c => c.Type == claimType && c.Value == claimValue);
        }
    }
}
