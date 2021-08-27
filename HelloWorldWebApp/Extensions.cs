// <copyright file="Extensions.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldWebApp.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HelloWorldWebApp
{
    /// <summary>
    /// Static Class for Extensions.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Database Migration method.
        /// </summary>
        /// <param name="host">This object IHost instance.</param>
        /// <returns>The modified IWebHost.</returns>
        public static IHost MigrateDatabase(this IHost host)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            if (env == "Production")
            {
                var serviceScopeFactory = (IServiceScopeFactory)host.Services.GetService(typeof(IServiceScopeFactory));
                using var scope = serviceScopeFactory.CreateScope();

                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<ApplicationContext>();

                dbContext.Database.Migrate();
            }

            return host;
        }

        /// <summary>
        /// Role and User creation method.
        /// </summary>
        /// <param name="host">This object IHost instance.</param>
        /// <returns>The modified IWebHost.</returns>
        public static IHost EnsureRolesAndUsersCreated(this IHost host)
        {
            var serviceScopeFactory = (IServiceScopeFactory)host.Services.GetService(typeof(IServiceScopeFactory));
            using var scope = serviceScopeFactory.CreateScope();

            var services = scope.ServiceProvider;

            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            EnsureRoleCreated(roleManager, "Operator").Wait();
            EnsureRoleCreated(roleManager, "Administrator").Wait();

            var env = Environment.GetEnvironmentVariable("DefaultAdminUserCredentials");
            if (env != null)
            {
                var adminEmail = env.Split(";")[0];
                var adminPassword = env.Split(";")[1];

                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

                var adminUser = EnsureUserCreated(userManager, adminEmail, adminPassword).Result;
                userManager.AddToRoleAsync(adminUser, "Administrator").Wait();
            }

            return host;
        }

        private static async Task<IdentityUser> EnsureUserCreated(UserManager<IdentityUser> userManager, string name, string password)
        {
            var user = await userManager.FindByNameAsync(name);

            if (user == null)
            {
                await userManager.CreateAsync(new IdentityUser(name));
                user = await userManager.FindByNameAsync(name);
                var tokenChangePassword = await userManager.GeneratePasswordResetTokenAsync(user);
                await userManager.ResetPasswordAsync(user, tokenChangePassword, password);

                if (!user.EmailConfirmed)
                {
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    await userManager.ConfirmEmailAsync(user, token);
                }
            }

            return user;
        }

        private static async Task<IdentityRole> EnsureRoleCreated(RoleManager<IdentityRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);

            if (role == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
                role = await roleManager.FindByNameAsync(roleName);
            }

            return role;
        }
    }
}
