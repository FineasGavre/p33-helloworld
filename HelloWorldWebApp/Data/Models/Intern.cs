// <copyright file="Intern.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;

namespace HelloWorldWebApp.Data.Models
{
    /// <summary>
    /// Intern model.
    /// </summary>
    public class Intern
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
        /// Gets or sets the Birthdate.
        /// </summary>
        public DateTime Birthdate { get; set; }

        /// <summary>
        /// Gets or sets the Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the Skills collection.
        /// </summary>
        public ICollection<Skill> Skills { get; set; }

        /// <summary>
        /// Compute the Age of the Intern.
        /// </summary>
        /// <returns>Intern age.</returns>
        public int GetAge()
        {
            var now = DateTime.Now;
            var age = now.Year - Birthdate.Year;

            if (Birthdate > now.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}
