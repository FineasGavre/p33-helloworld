// <copyright file="IUserService.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HelloWorldWebApp.Services.Impl
{
    /// <summary>
    /// Interface for User Service.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets all roles registered in the app.
        /// </summary>
        /// <returns>Enumerable of Roles.</returns>
        IEnumerable<IdentityRole> GetAllRoles();
        Task<IEnumerable<IdentityRole>> GetUserRoles(string userId);
    }
}