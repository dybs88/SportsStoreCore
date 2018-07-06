using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SportsStore.Models.Identity;

namespace SportsStore.Models.User
{
    public class UserIndexViewModel
    {
        public SportUser User { get; set; }
        public IEnumerable<string> UserRoles { get; set; }
    }
}
