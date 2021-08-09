// <copyright file="TeamMemberService.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldWebApp.Data;
using HelloWorldWebApp.Data.Models;

namespace HelloWorldWebApp
{
    /// <summary>
    /// Implementation of ITeamMemberService.
    /// </summary>
    public class TeamMemberService : ITeamMemberService
    {
        private ConcurrentDictionary<int, TeamMember> teamMembers;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamMemberService"/> class.
        /// </summary>
        public TeamMemberService()
        {
            teamMembers = new ConcurrentDictionary<int, TeamMember>();
        }

        /// <inheritdoc/>
        public IList<TeamMember> GetTeamMembers()
        {
            return teamMembers.Values.ToList();
        }

        /// <inheritdoc/>
        public void AddTeamMember(TeamMember teamMember)
        {
            teamMember.Id = findNextId();
            teamMembers.AddOrUpdate(teamMember.Id, teamMember, (id, existingTm) =>
            {
                existingTm.Name = teamMember.Name;
                return existingTm;
            });
        }

        /// <inheritdoc/>
        public void UpdateTeamMember(TeamMember teamMember)
        {
            teamMembers.AddOrUpdate(teamMember.Id, teamMember, (id, existingTm) =>
            {
                existingTm.Name = teamMember.Name;
                return existingTm;
            });
        }

        /// <inheritdoc/>
        public void DeleteTeamMember(int id)
        {
            teamMembers.Remove(id, out _);
        }

        private int findNextId()
        {
            if (teamMembers.Count == 0)
            {
                return 1;
            }

            return teamMembers.Keys.Max() + 1;
        }
    }
}
