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

namespace SportsStore.Infrastructure.Start
{
    public static class PermissionSeedData
    {
        public static async void PopulatePermissions(IApplicationBuilder app)
        {
            AppIdentityDbContext context = app.ApplicationServices.GetRequiredService<AppIdentityDbContext>();

            if(!context.Permissions.Any())
            {
                context.Permissions.AddRange
                    (new Permission { Value = SecurityPermssionValues.AddUser, Category = "Bezpieczeństwo", Description = "Uprawnienie umożliwające dodawanie użytkowników do bazy" },
                     new Permission { Value = SecurityPermssionValues.EditUser, Category = "Bezpieczeństwo", Description = "Uprawnienie umożliwające edycję użytkowników" },
                     new Permission { Value = SecurityPermssionValues.DeleteUser, Category = "Bezpieczeństwo", Description = "Uprawnienie umożliwające usuwanie użytkowników z bazy" },

                     new Permission { Value = SecurityPermssionValues.AddRole, Category = "Bezpieczeństwo", Description = "Uprawnienie umożliwające dodawanie ról do bazy" },
                     new Permission { Value = SecurityPermssionValues.EditRole, Category = "Bezpieczeństwo", Description = "Uprawnienie umożliwające edycję ról" },
                     new Permission { Value = SecurityPermssionValues.DeleteRole, Category = "Bezpieczeństwo", Description = "Uprawnienie umożliwające usuwanie ról z bazy" }
                    );
                context.SaveChanges();
            }

        }
    }
}
