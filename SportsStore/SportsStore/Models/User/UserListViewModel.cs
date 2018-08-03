using Microsoft.AspNetCore.Identity;
using SportsStore.Helpers;
using SportsStore.Models.Base;
using SportsStore.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.User
{
    public class UserListViewModel : BaseListViewModel
    {
        public IDictionary<SportUser, IEnumerable<string>> UsersWithRoles { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public string SelectedRole { get; set; }
        public UserListViewModel(IEnumerable<string> roles, int currentPage, int itemsPerPage, int totalItems)
        {
            Roles = roles;
            UsersWithRoles = new Dictionary<SportUser, IEnumerable<string>>();
            PageModel = new PageHelper { ItemPerPage = itemsPerPage, CurrentPage = currentPage, TotalItems = totalItems };
        }


    }
}
