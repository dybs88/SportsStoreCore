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
using SportsStore.DAL.Contexts;
using SportsStore.DAL.Repos;
using SportsStore.DAL.Repos.CustomerSchema;
using SportsStore.DAL.Repos.Security;
using SportsStore.Infrastructure.Policies;
using SportsStore.Infrastructure.Start;
using SportsStore.Models;
using SportsStore.Models.Cart;
using SportsStore.Models.DAL.Repos;
using SportsStore.Models.Identity;
using SportsStore.Models.OrderModels;

namespace SportsStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration["Data:SportsStoreProducts:connectionString"]));

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(Configuration["Data:SportsStoreIdentity:connectionString"]));
            services.AddIdentity<SportUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IAddressRepository, AddressRepository>();

            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();

            SecurityPolicies.AddSecurityPolicies(services);
            CustomerPolicies.AddCustomerPolicies(services);
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
                //routes.MapRoute
                //    (name: null,
                //     template: "{controller}/{action}/address{addressId:int}",
                //     defaults: new { controller = "Customer" });
                routes.MapRoute
                    (name: null,
                     template: "{action}/customer{customerId:int}",
                     defaults: new {controller = "Customer"});
                routes.MapRoute
                   (name: null,
                    template: "{category}/Strona{productPage:int}",
                    defaults: new {controller = "Product", action = "List"});

                routes.MapRoute
                   (name: null,
                    template: "Strona{productPage:int}",
                    defaults: new {controller = "Product", action = "List", productPage = 1});

                routes.MapRoute
                   (name: null,
                    template: "{category}",
                    defaults: new {controller = "Product", action = "List", productPage = 1});

                routes.MapRoute
                   (name: null,
                    template: "",
                    defaults: new {controller = "Product", action = "List", productPage = 1});

                routes.MapRoute
                   (name: null,
                    template: "{controller}/{action}/{id?}");
            });

            Migrate.ExecuteContextsMigrate(app);
            ProductSeedData.EnsurePopulated(app);
            PermissionSeedData.PopulatePermissions(app);
            //IdentitySeedData.PopulateIdentity(app);
        }
    }
}
