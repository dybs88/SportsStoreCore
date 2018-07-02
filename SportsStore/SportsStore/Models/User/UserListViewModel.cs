using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.User
{
    public class UserListViewModel
    {
        public IDictionary<IdentityUser, IEnumerable<string>> UsersWithRoles { get; set; }
        public IEnumerable<string> Roles { get; set; }

        public UserListViewModel()
        {
            UsersWithRoles = new Dictionary<IdentityUser, IEnumerable<string>>();
        }


    }
}
