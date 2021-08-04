// <copyright file="ITeamMemberStore.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System.Collections.Concurrent;
using System.Collections.Generic;

namespace HelloWorldWebApp.Data
{
    /// <summary>
    /// TeamMember Singleton Store.
    /// </summary>
    public interface ITeamMemberStore
    {
        /// <summary>
        /// Gets a list of TeamMembers.
        /// </summary>
        /// <returns>A list of TeamMembers.</returns>
        IList<string> GetTeamMembers();

        /// <summary>
        /// Add a TeamMember to the store.
        /// </summary>
        /// <param name="teamMemberName">TeamMember name.</param>
        void AddTeamMember(string teamMemberName);
    }
}