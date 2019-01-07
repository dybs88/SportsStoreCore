using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using SportsStore.Domain;
using SportsStore.Helpers;
using SportsStore.Infrastructure.Extensions;
using SportsStore.Infrastructure.Start.AppConfiguration;
using SportsStore.Infrastructure.Start.SeedDatas;

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
            services
                .AddAppSettings(Configuration)
                .AddJwtHandler(Configuration);

            AppSettings settings = services.GetAppSettings(Configuration);

            services.AddCors(options => {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.WithOrigins(settings.WebClientAddress)
                           .AllowAnyMethod()
                           .WithExposedHeaders("content-disposition")
                           .AllowAnyHeader()
                           .AllowCredentials()
                           .SetPreflightMaxAge(TimeSpan.FromSeconds(3600));
                });
            });
            services.Configure<IISOptions>(options => 
            {
                options.AutomaticAuthentication = false;
                options.ForwardClientCertificate = false;
            });

            services
                .AddSportsStoreSecurityModule()
                .AddSportsStoreDatabase(Configuration, Environment.EnvironmentName)
                .AddSportsStoreCustomerModule()
                .AddSportsStoreDictionariesModule()
                .AddSportsStoreSalesModule()
                .AddSportsStoreStoreModule();
         
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
            services.AddAntiforgery(o => o.HeaderName = "RequestVerificationToken");       
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("CorsPolicy");
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();
            app.UseMiddleware<Infrastructure.Middleware.SessionMiddleware>();
            app.UseMvc(routes =>
            {
                RouteMapping.MapRoutes(routes);
            });


            if(env.EnvironmentName != "TEST")
            {
                app.UseSportsStore();
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
