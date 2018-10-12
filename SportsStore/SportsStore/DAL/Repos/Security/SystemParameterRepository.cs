using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsStore.DAL.Contexts;
using SportsStore.Infrastructure.Exceptions;
using SportsStore.Models.Parameters;

namespace SportsStore.DAL.Repos.Security
{
    public class SystemParameterRepository : ISystemParameterRepository
    {
        private AppIdentityDbContext _context;

        public SystemParameterRepository(AppIdentityDbContext context)
        {
            _context = context;
        }

        public IQueryable<SystemParameter> Parameters => _context.Parameters;

        public SystemParameter GetParameter(string key)
        {
            return Parameters.First(sp => sp.Key == key);
        }

        public void SaveParameter(string key, string newValue)
        {
            SystemParameter dbEntry = _context.Parameters.FirstOrDefault(sp => sp.Key == key);

            if(dbEntry != null)
            {
                dbEntry.Value = newValue;
                _context.SaveChanges();
            }
            else
            {
                throw new ParameterNotExistException($"Parametr o kluczu {key} nie istnieje");
            }
        }
    }
}
