// <copyright file="Startup.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using HelloWorldWebApp.Data;
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
            services.AddDbContext<ApplicationContext>(options =>
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
                    connectionUri = connectionUri.Replace("postgres://", string.Empty);

                    var pgUserPass = connectionUri.Split("@")[0];
                    var pgHostPortDb = connectionUri.Split("@")[1];
                    var pgHostPort = pgHostPortDb.Split("/")[0];
                    var pgDb = pgHostPortDb.Split("/")[1];
                    var pgUser = pgUserPass.Split(":")[0];
                    var pgPass = pgUserPass.Split(":")[1];
                    var pgHost = pgHostPort.Split(":")[0];
                    var pgPort = pgHostPort.Split(":")[1];

                    connectionString = $"Host={pgHost};Port={pgPort};Username={pgUser};Password={pgPass};Database={pgDb};SSL Mode=Require;Trust Server Certificate=true";
                }

                options.UseNpgsql(connectionString);
            });

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>();

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddScoped<ITeamMemberService, TeamMemberService>();
            services.AddScoped<IWeatherService, WeatherService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hello World API", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddSignalR();

            if (Environment.GetEnvironmentVariable("ShouldCreateDefaultUsers") != null)
            {
                EnsureUsersCreated(services).Wait();
            }
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

                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static async Task EnsureUsersCreated(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var adminUser = await EnsureUserCreated(userManager, "fineasgavre@gmail.com", "TzfOh22_FCbjxXQt6U");
            var operatorUser = await EnsureUserCreated(userManager, "fineasgavre.admin@gmail.com", "TzfOh22_FCbjxXQt6U2");

            var adminRole = await EnsureRoleCreated(serviceProvider, "Administrator");
            var operatorRole = await EnsureRoleCreated(serviceProvider, "Operator");

            await userManager.AddToRoleAsync(adminUser, adminRole.Name);
            await userManager.AddToRoleAsync(operatorUser, operatorRole.Name);

            var users = await userManager.Users.ToListAsync();
            Console.WriteLine($"There are {users.Count} users now.");
        }

        private static async Task<IdentityUser> EnsureUserCreated(UserManager<IdentityUser> userManager, string name, string password)
        {
            var adminUser = await userManager.FindByNameAsync(name);
            if (adminUser == null)
            {
                await userManager.CreateAsync(new IdentityUser(name));
                adminUser = await userManager.FindByNameAsync(name);
                var tokenChangePassword = await userManager.GeneratePasswordResetTokenAsync(adminUser);

                var result = await userManager.ResetPasswordAsync(adminUser, tokenChangePassword, password);

                if (!adminUser.EmailConfirmed)
                {
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(adminUser);
                    await userManager.ConfirmEmailAsync(adminUser, token);
                }
            }

            return adminUser;
        }

        private static async Task<IdentityRole> EnsureRoleCreated(ServiceProvider serviceProvider, string roleName)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            var adminRole = await roleManager.FindByNameAsync(roleName);
            if (adminRole == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
                adminRole = await roleManager.FindByNameAsync(roleName);
            }

            return adminRole;
        }
    }
}
