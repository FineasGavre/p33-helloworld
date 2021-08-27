// <copyright file="TeamMemberHub.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldWebApp.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace HelloWorldWebApp.Hubs
{
    /// <summary>
    /// SignalR TeamMember Hub class.
    /// </summary>
    public class TeamMemberHub : Hub
    {
        private readonly ITeamMemberService teamMemberService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamMemberHub"/> class.
        /// </summary>
        /// <param name="teamMemberService">DI Injected TeamMemberService.</param>
        public TeamMemberHub(ITeamMemberService teamMemberService)
        {
            this.teamMemberService = teamMemberService;
        }

        /// <summary>
        /// Gets the TeamMembers in the application.
        /// </summary>
        /// <returns>An enumerable of the TeamMember entities.</returns>
        public IEnumerable<TeamMember> GetTeamMembers()
        {
            return teamMemberService.GetTeamMembers();
        }

        /// <summary>
        /// Adds a TeamMember to the application.
        /// </summary>
        /// <param name="teamMemberName">The name of the new TeamMember.</param>
        /// <returns>Completed task.</returns>
        [Authorize(Roles = "Operator")]
        public async Task AddTeamMember(string teamMemberName)
        {
            var teamMember = new TeamMember { Name = teamMemberName };
            teamMemberService.AddTeamMember(teamMember);
            await Clients.All.SendAsync("AddedTeamMember", teamMember);
        }

        /// <summary>
        /// Deletes a TeamMember from the application.
        /// </summary>
        /// <param name="teamMemberId">The id of the TeamMember to be deleted.</param>
        /// <returns>Completed task.</returns>
        [Authorize(Roles = "Operator")]
        public async Task DeleteTeamMember(int teamMemberId)
        {
            teamMemberService.DeleteTeamMember(teamMemberId);
            await Clients.All.SendAsync("DeletedTeamMember", teamMemberId);
        }

        /// <summary>
        /// Updates a TeamMember in the application.
        /// </summary>
        /// <param name="teamMember">The updated TeamMember.</param>
        /// <returns>Completed task.</returns>
        [Authorize(Roles = "Operator")]
        public async Task UpdateTeamMember(TeamMember teamMember)
        {
            teamMemberService.UpdateTeamMember(teamMember);
            await Clients.All.SendAsync("UpdatedTeamMember", teamMember);
        }
    }
}
