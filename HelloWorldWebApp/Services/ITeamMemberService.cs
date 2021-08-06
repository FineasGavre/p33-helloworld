// <copyright file="ITeamMemberService.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System.Collections.Generic;
using HelloWorldWebApp.Data.Models;

namespace HelloWorldWebApp
{
    /// <summary>
    /// Interface for the TeamMemberService.
    /// </summary>
    public interface ITeamMemberService
    {
        /// <summary>
        /// Adds a TeamMember to the database.
        /// </summary>
        /// <param name="teamMember">TeamMember to be added.</param>
        void AddTeamMember(TeamMember teamMember);

        /// <summary>
        /// Delete a TeamMember from the database.
        /// </summary>
        /// <param name="id">TeamMember ID to be deleted.</param>
        void DeleteTeamMember(int id);

        /// <summary>
        /// Get all TeamMembers in the database.
        /// </summary>
        /// <returns>A List with TeamMembers.</returns>
        IList<TeamMember> GetTeamMembers();

        /// <summary>
        /// Update a TeamMember in the database.
        /// </summary>
        /// <param name="teamMember">TeamMember to be updated.</param>
        void UpdateTeamMember(TeamMember teamMember);
    }
}