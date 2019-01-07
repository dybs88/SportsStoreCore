using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SportsStore.DAL.AbstractContexts;
using SportsStore.DAL.Contexts;
using SportsStore.DAL.Repos;
using SportsStore.DAL.Repos.CustomerSchema;
using SportsStore.DAL.Repos.DictionarySchema;
using SportsStore.DAL.Repos.Security;
using SportsStore.Helpers;
using SportsStore.Infrastructure.Policies;
using SportsStore.Models.Cart;
using SportsStore.Models.DAL.Repos.SalesSchema;
using SportsStore.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSportsStoreDatabase(this IServiceCollection services, IConfiguration config, string environmentName)
        {
            switch(environmentName)
            {
                case "PROD":
                {
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(config["Release:ConnectionStrings:SportsStoreProducts:connectionString"]));
                    services.AddDbContext<AppIdentityDbContext>(options =>
                        options.UseSqlServer(config["Release:ConnectionStrings:SportsStoreIdentity:connectionString"]));
                    services.AddIdentity<SportUser, IdentityRole>(options =>
                    {
                        options.User.RequireUniqueEmail = true;
                    })
                        .AddEntityFrameworkStores<AppIdentityDbContext>()
                        .AddDefaultTokenProviders();
                    services.AddDbContext<DictionaryDbContext>(options =>
                        options.UseSqlServer(config["Release:ConnectionStrings:SportsStoreDictionaries:connectionString"]));
                    break;
                }
                    
                case "DEV":
                {
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(config["Debug:ConnectionStrings:SportsStoreProducts:connectionString"]));
                    services.AddDbContext<AppIdentityDbContext>(options =>
                        options.UseSqlServer(config["Debug:ConnectionStrings:SportsStoreIdentity:connectionString"]));
                    services.AddIdentity<SportUser, IdentityRole>(options =>
                    {
                        options.User.RequireUniqueEmail = true;
                    })
                        .AddEntityFrameworkStores<AppIdentityDbContext>()
                        .AddDefaultTokenProviders();
                    services.AddDbContext<DictionaryDbContext>(options =>
                        options.UseSqlServer(config["Debug:ConnectionStrings:SportsStoreDictionaries:connectionString"]));
                    break;
                }
                   
                default:
                    break;
            }


            services.AddSingleton<IApplicationDbContext, ApplicationDbContext>();

            return services;
        }

        public static IServiceCollection AddSportsStoreSecurityModule(this IServiceCollection services)
        {
            services.AddTransient<ISportsStoreUserManager, SportsStoreUserManager>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<ISystemParameterRepository, SystemParameterRepository>();

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

        public static IServiceCollection AddAppSettings(this IServiceCollection services, IConfiguration config)
        {
            var appSettingsSection = config.GetSection("appSettings");
            services.Configure<AppSettings>(appSettingsSection);

            return services;
        }

        public static AppSettings GetAppSettings(this IServiceCollection services, IConfiguration config)
        {
            var appSettingsSection = config.GetSection("appSettings");
            return appSettingsSection.Get<AppSettings>();
        }

        public static IServiceCollection AddJwtHandler(this IServiceCollection services, IConfiguration config)
        {
            var settings = services.GetAppSettings(config);
            var key = Encoding.ASCII.GetBytes(settings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x => 
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userManager = context.HttpContext.RequestServices.GetRequiredService<ISportsStoreUserManager>();
                        var user = userManager.FindByIdAsync(context.Principal.Identity.Name);

                        if(user == null)
                        {
                            context.Fail("Brak autoryzacji");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            return services;
        }
    }
}
