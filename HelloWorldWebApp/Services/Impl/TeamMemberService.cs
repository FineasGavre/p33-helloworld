// <copyright file="TeamMemberService.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System;
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
        private readonly ApplicationContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamMemberService"/> class.
        /// </summary>
        /// <param name="context">DI ApplicationContext database.</param>
        public TeamMemberService(ApplicationContext context)
        {
            this.context = context;
        }

        /// <inheritdoc/>
        public IList<TeamMember> GetTeamMembers()
        {
            return context.TeamMembers.ToList();
        }

        /// <inheritdoc/>
        public void AddTeamMember(TeamMember teamMember)
        {
            context.TeamMembers.Add(teamMember);
            context.SaveChanges();
        }

        /// <inheritdoc/>
        public void UpdateTeamMember(TeamMember teamMember)
        {
            context.TeamMembers.Update(teamMember);
            context.SaveChanges();
        }

        /// <inheritdoc/>
        public void DeleteTeamMember(int id)
        {
            context.TeamMembers.Remove(context.TeamMembers.Find(id));
            context.SaveChanges();
        }
    }
}
