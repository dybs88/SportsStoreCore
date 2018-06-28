using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Models;
using SportsStore.Models.Cart;
using SportsStore.Models.Order;

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
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
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
                //     template: "Produkt{productId:int}",
                //     defaults: new {contoller = "Admin", action="Edit"}
                //    );
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

            SeedData.EnsurePopulated(app);
            SeedData.EnsurePopulatedIdentity(app);
        }
    }
}
