using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.User
{
    public class EditUserViewModel
    {
        public IdentityUser User { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
