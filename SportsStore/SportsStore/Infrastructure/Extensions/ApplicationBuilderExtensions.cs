using Microsoft.AspNetCore.Builder;
using SportsStore.Infrastructure.Start.SeedDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async void UseSportsStore(this IApplicationBuilder app)
        {
            await Migrate.ExecuteContextsMigrate(app);
            await DictionarySeedData.PopulateDictionaries(app);
            await ProductSeedData.EnsurePopulated(app);
            await PermissionSeedData.PopulatePermissions(app);
            await IdentitySeedData.PopulateIdentity(app);
            await SystemParametersSeedData.PopulateParameters(app);
        }
    }
}
