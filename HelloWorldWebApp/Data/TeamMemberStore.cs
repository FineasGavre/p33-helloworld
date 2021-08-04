// <copyright file="TeamMemberStore.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldWebApp.Data
{
    /// <summary>
    /// TeamMemberStore.
    /// </summary>
    public class TeamMemberStore : ITeamMemberStore
    {
        private readonly BlockingCollection<string> teamMembers = new ();

        /// <inheritdoc/>
        public IList<string> GetTeamMembers() => teamMembers.ToList<string>();

        /// <inheritdoc/>
        public void AddTeamMember(string teamMemberName)
        {
            teamMembers.Add(teamMemberName);
        }
    }
}
