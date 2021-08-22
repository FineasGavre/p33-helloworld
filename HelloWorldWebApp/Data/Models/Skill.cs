// <copyright file="Skill.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldWebApp.Data.Models
{
    /// <summary>
    /// Skill model.
    /// </summary>
    public class Skill
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
        /// Gets or sets the Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the the SkillMatrixUrl.
        /// </summary>
        public string SkillMatrixUrl { get; set; }

        /// <summary>
        /// Gets or sets the Intern.
        /// </summary>
        public Intern Intern { get; set; }

        /// <summary>
        /// Gets or sets the Intern Id.
        /// </summary>
        public int InternId { get; set; }

        /// <summary>
        /// Gets or sets the LibraryResource collection.
        /// </summary>
        public ICollection<LibraryResource> LibraryResources { get; set; }
    }
}
