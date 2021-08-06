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
            var teamMembers = teamMemberService.GetTeamMembers().ToList();

            return new JsonResult(teamMembers);
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
            return Ok();
        }

        /// <summary>
        /// UpdateTeamMember action.
        /// </summary>
        /// <param name="id">Id of the TeamMember to be updated.</param>
        /// <param name="name">Name of the TeamMember to be updated.</param>
        /// <returns>HTTP Status code 200.</returns>
        [HttpPut]
        public IActionResult UpdateTeamMember(int id, string name)
        {
            teamMemberService.UpdateTeamMember(new TeamMember { Id = id, Name = name });
            return Ok();
        }

        /// <summary>
        /// DeleteTeamMember action.
        /// </summary>
        /// <param name="id">Id of the TeamMember to be deleted.</param>
        /// <returns>HTTP Status code 200.</returns>
        [HttpDelete]
        public IActionResult DeleteTeamMember(int id)
        {
            teamMemberService.DeleteTeamMember(id);
            return Ok();
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