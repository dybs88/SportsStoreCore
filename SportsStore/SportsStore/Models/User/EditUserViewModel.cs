using Microsoft.AspNetCore.Identity;
using SportsStore.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.User
{
    public class EditUserViewModel
    {
        public SportUser User { get; set; }

        public IEnumerable<string> SelectedRoles { get; set; }
    }
}
