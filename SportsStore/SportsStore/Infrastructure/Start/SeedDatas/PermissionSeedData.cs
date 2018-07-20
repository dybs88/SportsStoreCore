using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.DAL.Contexts;
using SportsStore.Domain;
using SportsStore.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Start.SeedDatas
{
    public static class PermissionSeedData
    {
        public static void PopulatePermissions(IApplicationBuilder app)
        {
            AppIdentityDbContext context = app.ApplicationServices.GetRequiredService<AppIdentityDbContext>();

            if(!context.Permissions.Any())
            {
                context.Permissions.AddRange
                    (new Permission { Value = SecurityPermissionValues.AddUser, Category = "Bezpieczeństwo", Description = "Uprawnienie umożliwające dodawanie użytkowników do bazy" },
                     new Permission { Value = SecurityPermissionValues.EditUser, Category = "Bezpieczeństwo", Description = "Uprawnienie umożliwające edycję użytkowników" },
                     new Permission { Value = SecurityPermissionValues.DeleteUser, Category = "Bezpieczeństwo", Description = "Uprawnienie umożliwające usuwanie użytkowników z bazy" },

                     new Permission { Value = SecurityPermissionValues.AddRole, Category = "Bezpieczeństwo", Description = "Uprawnienie umożliwające dodawanie ról do bazy" },
                     new Permission { Value = SecurityPermissionValues.EditRole, Category = "Bezpieczeństwo", Description = "Uprawnienie umożliwające edycję ról" },
                     new Permission { Value = SecurityPermissionValues.DeleteRole, Category = "Bezpieczeństwo", Description = "Uprawnienie umożliwające usuwanie ról z bazy" }
                    );
                context.SaveChanges();
            }

        }
    }
}
