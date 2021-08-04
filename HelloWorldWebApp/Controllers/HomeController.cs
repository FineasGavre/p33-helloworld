// <copyright file="HomeController.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System.Diagnostics;
using System.Linq;
using HelloWorldWebApp.Data;
using HelloWorldWebApp.Data.Models;
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
        private readonly ILogger<HomeController> logger;
        private readonly ITeamMemberService teamMemberService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">App Logger.</param>
        /// <param name="teamMemberService">Team Member service.</param>
        public HomeController(ILogger<HomeController> logger, ITeamMemberService teamMemberService)
        {
            this.logger = logger;
            this.teamMemberService = teamMemberService;
        }

        /// <summary>
        /// Index action.
        /// </summary>
        /// <returns>Index.cshtml view with model.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// GetTeamMembers action.
        /// </summary>
        /// <returns>JSON object that contains TeamMember[].</returns>
        public IActionResult GetTeamMembers()
        {
            var teamMembers = teamMemberService.GetTeamMembers().Select(teamMember => teamMember.Name).ToList();

            return new JsonResult(new IndexViewModel { TeamMembers = teamMembers });
        }

        /// <summary>
        /// AddTeamMember action.
        /// </summary>
        /// <param name="addTeamMemberInput">Input model for action.</param>
        /// <returns>HTTP Response status code 200.</returns>
        [HttpPost]
        public IActionResult AddTeamMember([Bind("TeamMemberName")] AddTeamMemberInput addTeamMemberInput)
        {
            teamMemberService.AddTeamMember(new TeamMember { Name = addTeamMemberInput.TeamMemberName });
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