using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
using SportsStore.Infrastructure.Start.AppConfiguration;
using SportsStore.Infrastructure.Start.SeedDatas;
using SportsStore.Models.Cart;
using SportsStore.Models.DAL.Repos.SalesSchema;
using SportsStore.Models.Identity;

namespace SportsStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IHostingEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration["Data:SportsStoreProducts:connectionString"]));

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(Configuration["Data:SportsStoreIdentity:connectionString"]));
            services.AddIdentity<SportUser, IdentityRole>(options => 
            {
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();
            services.AddDbContext<DictionaryDbContext>(options =>
                options.UseSqlServer(Configuration["Data:SportsStoreDictionaries:connectionString"]));

            services.AddTransient<IApplicationDbContext, ApplicationDbContext>();
            services.AddTransient<ISportsStoreUserManager, SportsStoreUserManager>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<IDocumentTypeRepository, DocumentTypeRepository>();

            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

            SecurityPolicies.AddSecurityPolicies(services);
            CustomerPolicies.AddCustomerPolicies(services);
            SalesPolicies.AddSalesPolicies(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                RouteMapping.MapRoutes(routes);
            });

            if(env.EnvironmentName != "TEST")
            {
                Migrate.ExecuteContextsMigrate(app);
                ProductSeedData.EnsurePopulated(app);
                PermissionSeedData.PopulatePermissions(app);
                IdentitySeedData.PopulateIdentity(app);
                DictionarySeddData.PopulateDictionaries(app);
            }
        }

        public class TestStartup : Startup
        {
            public TestStartup(IConfiguration configuration, IHostingEnvironment env) : base(configuration, env)
            {
                env.EnvironmentName = "TEST";
            }
        }
    }
}
