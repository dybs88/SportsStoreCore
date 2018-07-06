using Microsoft.AspNetCore.Identity;
using SportsStore.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.Role
{
    public class EditRoleViewModel
    {
        public IdentityRole Role { get; set; }
        public List<SportUser> UsersInRole { get; set; }
        public List<string> AvaibleClaims { get; set; }

        public List<string> SelectedPermission { get; set; }

        public EditRoleViewModel()
        {
            SelectedPermission = new List<string>();
        }
    }
}
