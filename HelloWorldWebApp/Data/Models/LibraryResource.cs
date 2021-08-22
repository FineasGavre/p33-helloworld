// <copyright file="LibraryResource.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldWebApp.Data.Models
{
    /// <summary>
    /// LibraryResource model.
    /// </summary>
    public class LibraryResource
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Recommendation.
        /// </summary>
        public string Recommendation { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Gets or sets the Skill.
        /// </summary>
        public Skill Skill { get; set; }

        /// <summary>
        /// Gets or sets the SkillId.
        /// </summary>
        public int SkillId { get; set; }
    }
}
