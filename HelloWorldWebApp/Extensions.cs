// <copyright file="Extensions.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldWebApp.Data;
using Microsoft.AspNetCore.Hosting;
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
    }
}
