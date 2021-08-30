// <copyright file="RolesHub.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace HelloWorldWebApp.Hubs
{
    /// <summary>
    /// SignalR Hub for User Roles.
    /// </summary>
    public class RolesHub : Hub
    {
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="RolesHub"/> class.
        /// </summary>
        /// <param name="serviceProvider">DI ServiceProvider.</param>
        public RolesHub(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Get all roles registered in the app.
        /// </summary>
        /// <returns>Enumerable of Roles.</returns>
        public IEnumerable<IdentityRole> GetAllRoles()
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            return roleManager.Roles;
        }

        /// <summary>
        /// Get roles for an user.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>Enumerable of Roles.</returns>
        public async Task<IEnumerable<IdentityRole>> GetUserRoles(string userId)
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(userId);
            var userRoles = await userManager.GetRolesAsync(user);
            var roles = await Task.WhenAll(userRoles.Select(x => roleManager.FindByNameAsync(x)));

            return roles;
        }

        /// <summary>
        /// Assigns a user to a role.
        /// </summary>
        /// <param name="roleId">Role id.</param>
        /// <param name="userId">User id.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Authorize(Roles = "Administrator")]
        public async Task AssignRoleToUser(string roleId, string userId)
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                await DisplayError("The requested user does not exist.");
                return;
            }

            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                await DisplayError("The requested role does not exist.");
                return;
            }

            if (await userManager.IsInRoleAsync(user, role.Name))
            {
                await DisplayError("The user already has this role.");
                return;
            }

            await userManager.AddToRoleAsync(user, role.Name);
            await DisplayWarning("Role changes will take effect only after the user logs in again. You can invalidate the user's sessions in this page.");
            await Clients.All.SendAsync("UserRoleAdded", userId, role);
        }

        /// <summary>
        /// Unassign Role from User.
        /// </summary>
        /// <param name="roleId">Role id.</param>
        /// <param name="userId">User id.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Authorize(Roles = "Administrator")]
        public async Task UnassignRoleFromUser(string roleId, string userId)
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                await DisplayError("The requested user does not exist.");
                return;
            }

            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                await DisplayError("The requested role does not exist.");
                return;
            }

            if (!(await userManager.IsInRoleAsync(user, role.Name)))
            {
                await DisplayError("The user does not have this role.");
                return;
            }

            if (role.Name == "Administrator" && (await userManager.GetUsersInRoleAsync("Administrator")).Count == 1)
            {
                await DisplayError("Cannot unassign the last user with Administrator role.");
                return;
            }

            await userManager.RemoveFromRoleAsync(user, role.Name);
            await DisplayWarning("Role changes will take effect only after the user logs in again. You can invalidate the user's sessions in this page.");
            await Clients.All.SendAsync("UserRoleRemoved", userId, role);
        }

        /// <summary>
        /// Invalidate a user's sessions.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>Task.</returns>
        [Authorize(Roles = "Administrator")]
        public async Task InvalidateUserSessions(string userId)
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                await DisplayError("The requested user does not exist.");
                return;
            }

            await userManager.UpdateSecurityStampAsync(user);
            await DisplayWarning("User session invalidated.");
        }

        private async Task DisplayError(string message)
        {
            await Clients.Caller.SendAsync("DisplayError", message);
        }

        private async Task DisplayWarning(string message)
        {
            await Clients.Caller.SendAsync("DisplayWarning", message);
        }
    }
}
