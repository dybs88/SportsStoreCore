using SportsStore.DAL.Contexts;
using SportsStore.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.DAL.Repos.Security
{
    public class PermissionRepository : IPermissionRepository
    {
        private AppIdentityDbContext _context;

        public PermissionRepository(AppIdentityDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Permission> Permissions { get { return _context.Permissions; } }
    }
}
