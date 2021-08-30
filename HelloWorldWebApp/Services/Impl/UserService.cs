// <copyright file="UserService.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HelloWorldWebApp.Services.Impl
{
    /// <summary>
    /// Implementation for IUserService.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userManager">DI UserService.</param>
        /// <param name="roleManager">DI RoleManager.</param>
        public UserService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return roleManager.Roles.ToList();
        }

        public async Task<IEnumerable<IdentityRole>> GetUserRoles(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            var userRoles = await userManager.GetRolesAsync(user);
            var roles = await Task.WhenAll(userRoles.Select(x => roleManager.FindByNameAsync(x)));

            return roles;
        }
    }
}
