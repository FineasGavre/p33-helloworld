// <copyright file="UsersController.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldWebApp.Controllers
{
    /// <summary>
    /// Controller for the application Users.
    /// </summary>
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="userManager">DI injected UserManager.</param>
        public UsersController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        /// <summary>
        /// View all users.
        /// </summary>
        /// <returns>View.</returns>
        public ActionResult Index()
        {
            return View(userManager.Users.OrderBy(x => x.UserName));
        }

        /// <summary>
        /// View a specific user.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <returns>View.</returns>
        public async Task<ActionResult> DetailsAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            return View(user);
        }
    }
}
