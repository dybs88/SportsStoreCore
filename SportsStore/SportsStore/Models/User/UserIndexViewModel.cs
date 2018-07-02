using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SportsStore.Models.User
{
    public class UserIndexViewModel
    {
        public IdentityUser User { get; set; }
        public IEnumerable<string> UserRoles { get; set; }
    }
}
