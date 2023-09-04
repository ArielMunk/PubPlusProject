using BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services;
using System;
using System.Collections.Generic;
using static Services.Auth;
using static Services.Common;

namespace PubPlusProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            SettingsFileService.Configuration = Configuration;

            services.AddSingleton<IAuthorizationHandler, PoliciesAuthorizationHandler>();

            services.AddAuthorization(config =>
            {
                config.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                foreach (string section in new List<string>()
                {
                    "PrivateArea"
                })
                {
                    config.AddPolicy(section, policyBuilder =>
                    {
                        policyBuilder.AuthorizeRequireSection(section);
                    });
                }
            });


            services.AddAuthentication("CookieAuthentication")
           .AddCookie("CookieAuthentication", config =>
           {
               config.Cookie.Name = "19EE38CB-4D21-4C8D-8BFC-9DD8A3CE37C6";
               config.LoginPath = "/";
               config.ExpireTimeSpan = new TimeSpan(29, 23, 59, 59, 999);
               config.Cookie.MaxAge = new TimeSpan(29, 23, 59, 59, 999);
               //config.Cookie.Domain = "localhost";
           });


            services.AddHttpContextAccessor();


            services.AddControllersWithViews();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            //app.UseCors(builder =>
            //{
            //    builder.WithOrigins("http://my-work-status.com")
            //           .AllowAnyHeader()
            //           .AllowAnyMethod();
            //});

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            // who are you?  
            app.UseAuthentication();

            // are you allowed?  
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
