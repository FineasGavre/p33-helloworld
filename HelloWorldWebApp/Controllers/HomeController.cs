using HelloWorldWebApp.Data;
using HelloWorldWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITeamMemberStore _teamMemberStore;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ITeamMemberStore teamMemberStore)
        {
            _logger = logger;
            _teamMemberStore = teamMemberStore;
        }

        public IActionResult Index()
        {
            return View(new IndexViewModel { TeamMembers = _teamMemberStore.GetTeamMembers() });
        }

        [HttpPost]
        public IActionResult AddTeamMember([Bind("TeamMemberName")] AddTeamMemberInput addTeamMemberInput)
        {
            _teamMemberStore.AddTeamMember(addTeamMemberInput.TeamMemberName);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}