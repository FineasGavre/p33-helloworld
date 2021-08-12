// <copyright file="TeamCompositionService.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldWebApp.Services
{
    /// <summary>
    /// Service for TeamCompositionManagement.
    /// </summary>
    public class TeamCompositionService : ITeamCompositionService
    {
        private readonly ITeamMemberService teamMemberService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamCompositionService"/> class.
        /// </summary>
        /// <param name="teamMemberService">TeamMemberService DI.</param>
        public TeamCompositionService(ITeamMemberService teamMemberService)
        {
            this.teamMemberService = teamMemberService;
        }

        /// <summary>
        /// Gets the initials of a team.
        /// </summary>
        /// <returns>String with initials.</returns>
        public string GetInitialsOfTeam()
        {
            var initials = teamMemberService.GetTeamMembers()
                .OrderBy(tm => tm.Id)
                .Aggregate(string.Empty, (value, member) =>
                {
                    return value += member.Name.ToUpper()[0];
                });

            return initials;
        }
    }
}
