using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.User
{
    public class UserListViewModel
    {
        public UserListViewModel()
        {
            UsersWithRoles = new Dictionary<IdentityUser, IEnumerable<string>>();
        }
        public IDictionary<IdentityUser, IEnumerable<string>> UsersWithRoles { get; set; }
    }
}
