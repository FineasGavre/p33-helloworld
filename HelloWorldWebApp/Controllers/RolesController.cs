// <copyright file="RolesController.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldWebApp.Controllers
{
    /// <summary>
    /// Controller for Identity Roles.
    /// </summary>
    [Authorize]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="RolesController"/> class.
        /// </summary>
        /// <param name="roleManager">DI RoleManager.</param>
        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        /// <summary>
        /// Get roles.
        /// </summary>
        /// <returns>View with model.</returns>
        public IActionResult Index()
        {
            return View(roleManager.Roles);
        }

        /// <summary>
        /// Get create role form.
        /// </summary>
        /// <returns>View.</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Create a Role.
        /// </summary>
        /// <param name="role">Role to be created.</param>
        /// <returns>Redirection to Index.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync(IdentityRole role)
        {
            try
            {
                await roleManager.CreateAsync(role);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
