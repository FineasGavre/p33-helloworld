// <copyright file="ITeamCompositionService.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

namespace HelloWorldWebApp.Services
{
    /// <summary>
    /// Interface for Team Composition Service.
    /// </summary>
    public interface ITeamCompositionService
    {
        /// <summary>
        /// Computes the initials of a team.
        /// </summary>
        /// <returns>A string that contains the initials of a team.</returns>
        string GetInitialsOfTeam();
    }
}