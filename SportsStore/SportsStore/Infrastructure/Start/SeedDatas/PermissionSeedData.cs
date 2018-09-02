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
        static AppIdentityDbContext context;

        public static void PopulatePermissions(IApplicationBuilder app)
        {
            context = app.ApplicationServices.GetRequiredService<AppIdentityDbContext>();

            AddPermission(SecurityPermissionValues.ViewRole, SecurityPermissionCategories.Security, "Uprawnienie umożliwiające podgląd ról");
            AddPermission(SecurityPermissionValues.AddRole, SecurityPermissionCategories.Security, "Uprawnienie umożliwiające dodawanie nowych ról");
            AddPermission(SecurityPermissionValues.EditRole, SecurityPermissionCategories.Security, "Uprawnienie umożliwiające edycję ról");
            AddPermission(SecurityPermissionValues.DeleteRole, SecurityPermissionCategories.Security, "Uprawnienie umożliwiające usuwanie ról");

            AddPermission(SecurityPermissionValues.ViewUser, SecurityPermissionCategories.Security, "Uprawnienie umożliwiające podgląd użytkowników");
            AddPermission(SecurityPermissionValues.AddUser, SecurityPermissionCategories.Security, "Uprawnienie umożliwiające dodawanie nowych użytkowników");
            AddPermission(SecurityPermissionValues.EditUser, SecurityPermissionCategories.Security, "Uprawnienie umożliwiające edycję użytkowników");
            AddPermission(SecurityPermissionValues.DeleteUser, SecurityPermissionCategories.Security, "Uprawnienie umożliwiające usuwanie użytkowników");

            AddPermission(CustomerPermissionValues.ViewCustomer, SecurityPermissionCategories.Customer, "Uprawnienie umożliwiające podgląd klientów");
            AddPermission(CustomerPermissionValues.AddCustomer, SecurityPermissionCategories.Customer, "Uprawnienie umożliwiające dodawanie nowych klientów");
            AddPermission(CustomerPermissionValues.EditCustomer, SecurityPermissionCategories.Customer, "Uprawnienie umożliwiające edycję klientów");
            AddPermission(CustomerPermissionValues.DeleteCustomer, SecurityPermissionCategories.Customer, "Uprawnienie umożliwiające usuwanie klientów");

            AddPermission(CustomerPermissionValues.ViewAddress, SecurityPermissionCategories.Customer, "Uprawnienie umożliwiające podgląd adresów");
            AddPermission(CustomerPermissionValues.AddAddress, SecurityPermissionCategories.Customer, "Uprawnienie umożliwiające dodawanie nowych adresów");
            AddPermission(CustomerPermissionValues.EditAddress, SecurityPermissionCategories.Customer, "Uprawnienie umożliwiające edycję adresów");
            AddPermission(CustomerPermissionValues.DeleteAddress, SecurityPermissionCategories.Customer, "Uprawnienie umożliwiające usuwanie adresów");

            AddPermission(SalesPermissionValues.ViewOrder, SecurityPermissionCategories.Sales, "Uprawnienie umożliwiające podgląd zamówień");
            AddPermission(SalesPermissionValues.AddOrder, SecurityPermissionCategories.Sales, "Uprawnienie umożliwiające dodawanie nowych zamówień");
            AddPermission(SalesPermissionValues.EditOrder, SecurityPermissionCategories.Sales, "Uprawnienie umożliwiające edycję zamówień");
            AddPermission(SalesPermissionValues.DeleteOrder, SecurityPermissionCategories.Sales, "Uprawnienie umożliwiające usuwanie zamówień");

            context.SaveChanges();

        }

        private static void AddPermission(string permissionValue, string category, string description)
        {
            if (!context.Permissions.Any(p => p.Value == permissionValue))
            {
                context.Permissions.Add(new Permission
                {
                    Value = permissionValue,
                    Category = category,
                    Description = description
                });
            }
        }
    }
}
