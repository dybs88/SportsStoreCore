using Microsoft.AspNetCore.Identity;
using SportsStore.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.User
{
    public class UserListViewModel
    {
        public IDictionary<SportUser, IEnumerable<string>> UsersWithRoles { get; set; }
        public IEnumerable<string> Roles { get; set; }

        public string SelectedRole { get; set; }

        public UserListViewModel()
        {
            UsersWithRoles = new Dictionary<SportUser, IEnumerable<string>>();
        }


    }
}
