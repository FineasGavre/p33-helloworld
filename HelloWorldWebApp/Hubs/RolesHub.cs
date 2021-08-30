// <copyright file="RolesHub.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldWebApp.Services.Impl;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace HelloWorldWebApp.Hubs
{
    /// <summary>
    /// SignalR Hub for User Roles.
    /// </summary>
    public class RolesHub : Hub
    {
        private readonly IUserService userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RolesHub"/> class.
        /// </summary>
        /// <param name="userManager">DI UserManager.</param>
        /// <param name="roleManager">DI RoleManager.</param>
        public RolesHub(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Get all roles registered in the app.
        /// </summary>
        /// <returns>Enumerable of Roles.</returns>
        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return userService.GetAllRoles();
        }

        /// <summary>
        /// Get roles for an user.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>Enumerable of Roles.</returns>
        public async Task<IEnumerable<IdentityRole>> GetUserRoles(string userId)
        {
            return await userService.GetUserRoles(userId);
        }

/*        /// <summary>
        /// Assigns a user to a role.
        /// </summary>
        /// <param name="roleId">Role id.</param>
        /// <param name="userId">User id.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [Authorize(Roles = "Administrator")]
        public async Task AssignRoleToUser(string roleId, string userId)
        {
            
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
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                await DisplayError("The requested user does not exist.");
                return;
            }

            await userManager.UpdateSecurityStampAsync(user);
            await DisplayWarning("User session invalidated.");
        }*/

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
