using SportsStore.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.DAL.Repos.Security
{
    public interface IPermissionRepository
    {
        IEnumerable<Permission> Permissions { get; }
    }
}
