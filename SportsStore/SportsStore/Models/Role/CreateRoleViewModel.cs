using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.Role
{
    public class CreateRoleViewModel
    {
        public IdentityRole Role { get; set; }
        public List<string> SelectedPermission { get; set; }
    }
}
