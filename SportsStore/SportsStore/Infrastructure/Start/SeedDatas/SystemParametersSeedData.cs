using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.DAL.Contexts;
using SportsStore.Domain;
using SportsStore.Models.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Start.SeedDatas
{
    public class SystemParametersSeedData
    {
        public static async Task PopulateParameters(IApplicationBuilder app)
        {
            AppIdentityDbContext _context = app.ApplicationServices.GetRequiredService<AppIdentityDbContext>();

            if(!_context.Parameters.Any(sp => sp.Key == SystemParameters.ProductPriceType))
            {
                SystemParameter productPriceTypeParameter = new SystemParameter { Key = SystemParameters.ProductPriceType, Value = PriceType.Gross.ToString() };
                _context.Parameters.Add(productPriceTypeParameter);
            }

            _context.SaveChanges();
        }
    }
}
