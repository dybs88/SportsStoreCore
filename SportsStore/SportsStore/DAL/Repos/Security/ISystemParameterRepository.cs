using SportsStore.Models.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.DAL.Repos.Security
{
    public interface ISystemParameterRepository
    {
        IQueryable<SystemParameter> Parameters { get; }

        SystemParameter GetParameter(string key);

        void SaveParameter(string key, string newValue);
    }
}
