// <copyright file="AddTeamMemberInput.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldWebApp.Models
{
    /// <summary>
    /// Input Model for the AddTeamMember action.
    /// </summary>
    public class AddTeamMemberInput
    {
        /// <summary>
        /// Gets or sets the TeamMemberName.
        /// </summary>
        public string TeamMemberName { get; set; }
    }
}
