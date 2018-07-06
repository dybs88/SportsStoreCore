﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Start
{
    public static class Migrate
    {
        public static void ExecuteContextsMigrate(IApplicationBuilder app)
        {
            ApplicationDbContext appContext = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            appContext.Database.Migrate();

            AppIdentityDbContext identityContext = app.ApplicationServices.GetRequiredService<AppIdentityDbContext>();
            identityContext.Database.Migrate();
        }
    }
}
