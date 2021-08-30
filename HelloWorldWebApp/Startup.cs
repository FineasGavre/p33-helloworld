// <copyright file="Startup.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using HelloWorldWebApp.Data;
using HelloWorldWebApp.Helpers;
using HelloWorldWebApp.Hubs;
using HelloWorldWebApp.Services;
using HelloWorldWebApp.Services.Impl;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace HelloWorldWebApp
{
    /// <summary>
    /// ASP.NET Startup class.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">Configuration object.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the Configuration object.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Services collection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<ApplicationContext>(
                options =>
                {
                    var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    string connectionString;

                    if (env == "Development")
                    {
                        connectionString = Configuration.GetConnectionString("LocalPostgresContext");
                    }
                    else
                    {
                        var connectionUri = Environment.GetEnvironmentVariable("DATABASE_URL");
                        connectionString = PostgresConnectionStringConverter.ConvertPostgresUriToNpgsqlConnectionString(connectionUri);
                    }

                    options.UseNpgsql(connectionString);
                }, ServiceLifetime.Transient);

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddScoped<ITeamMemberService, TeamMemberService>();
            services.AddScoped<IWeatherService, WeatherService>();
            services.AddScoped<IClientNotificationService, ClientNotificationService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hello World API", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddSignalR();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">ApplicationBuilder object.</param>
        /// <param name="env">WebHostEnvironment object.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
                app.UseBrowserLink();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "HelloWorldApi");
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<TeamMemberHub>("/hubs/teammember");
                endpoints.MapHub<RolesHub>("/hubs/roles");
                endpoints.MapHub<ClientNotificationHub>("/hubs/notifications");

                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
