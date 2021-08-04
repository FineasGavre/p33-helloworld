// <copyright file="HomeController.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System.Diagnostics;
using HelloWorldWebApp.Data;
using HelloWorldWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HelloWorldWebApp.Controllers
{
    /// <summary>
    /// Main app controller.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ITeamMemberStore teamMemberStore;
        private readonly ILogger<HomeController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">App Logger.</param>
        /// <param name="teamMemberStore">TeamMember store.</param>
        public HomeController(ILogger<HomeController> logger, ITeamMemberStore teamMemberStore)
        {
            this.logger = logger;
            this.teamMemberStore = teamMemberStore;
        }

        /// <summary>
        /// Index action.
        /// </summary>
        /// <returns>Index.cshtml view with model.</returns>
        public IActionResult Index()
        {
            return View(new IndexViewModel { TeamMembers = teamMemberStore.GetTeamMembers() });
        }

        /// <summary>
        /// AddTeamMember action.
        /// </summary>
        /// <param name="addTeamMemberInput">Input model for action.</param>
        /// <returns>HTTP Response status code 200.</returns>
        [HttpPost]
        public IActionResult AddTeamMember([Bind("TeamMemberName")] AddTeamMemberInput addTeamMemberInput)
        {
            teamMemberStore.AddTeamMember(addTeamMemberInput.TeamMemberName);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Privacy action.
        /// </summary>
        /// <returns>Privacy.cshtml view.</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Error action.
        /// </summary>
        /// <returns>Error view.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}