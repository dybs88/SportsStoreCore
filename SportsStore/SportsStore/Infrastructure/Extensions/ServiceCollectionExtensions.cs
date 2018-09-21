using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.DAL.AbstractContexts;
using SportsStore.DAL.Contexts;
using SportsStore.DAL.Repos;
using SportsStore.DAL.Repos.CustomerSchema;
using SportsStore.DAL.Repos.DictionarySchema;
using SportsStore.DAL.Repos.Security;
using SportsStore.Infrastructure.Policies;
using SportsStore.Models.Cart;
using SportsStore.Models.DAL.Repos.SalesSchema;
using SportsStore.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSportsStoreDatabase(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(config["Data:SportsStoreProducts:connectionString"]));

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(config["Data:SportsStoreIdentity:connectionString"]));
            services.AddIdentity<SportUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();
            services.AddDbContext<DictionaryDbContext>(options =>
                options.UseSqlServer(config["Data:SportsStoreDictionaries:connectionString"]));

            services.AddTransient<IApplicationDbContext, ApplicationDbContext>();

            return services;
        }

        public static IServiceCollection AddSportsStoreSecurityModule(this IServiceCollection services)
        {
            services.AddTransient<ISportsStoreUserManager, SportsStoreUserManager>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();

            SecurityPolicies.AddSecurityPolicies(services);

            return services;
        }

        public static IServiceCollection AddSportsStoreCustomerModule(this IServiceCollection services)
        {
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IAddressRepository, AddressRepository>();

            CustomerPolicies.AddCustomerPolicies(services);

            return services;
        }

        public static IServiceCollection AddSportsStoreDictionariesModule(this IServiceCollection services)
        {
            services.AddTransient<IDocumentTypeRepository, DocumentTypeRepository>();
            services.AddTransient<IVatRateRepository, VatRateRepository>();
            services.AddTransient<IDictionaryContainer, DictionaryContainer>();

            return services;
        }

        public static IServiceCollection AddSportsStoreSalesModule(this IServiceCollection services)
        {
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));

            SalesPolicies.AddSalesPolicies(services);

            return services;
        }

        public static IServiceCollection AddSportsStoreStoreModule(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
