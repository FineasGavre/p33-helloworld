// <copyright file="ApplicationContext.cs" company="PRINCIPAL33">
// Copyright (c) PRINCIPAL33. All rights reserved.
// </copyright>

using System;
using HelloWorldWebApp.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HelloWorldWebApp.Data
{
    /// <summary>
    /// DbContext for the current Application.
    /// </summary>
    public class ApplicationContext : IdentityDbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationContext"/> class.
        /// </summary>
        /// <param name="options">Options for the DbContext.</param>
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the TeamMembers DbSet.
        /// </summary>
        public DbSet<TeamMember> TeamMembers { get; set; }

        /// <summary>
        /// Gets or sets the Interns DbSet.
        /// </summary>
        public DbSet<Intern> Interns { get; set; }

        /// <summary>
        /// Gets or sets the Skills DbSet.
        /// </summary>
        public DbSet<Skill> Skills { get; set; }

        /// <summary>
        /// Gets or sets the LibraryResources DbSet.
        /// </summary>
        public DbSet<LibraryResource> LibraryResources { get; set; }

        /// <summary>
        /// Override OnModelCreating from DbContext to link Entity to table.
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TeamMember>().ToTable("TeamMembers");
            modelBuilder.Entity<Intern>().ToTable("Interns");
            modelBuilder.Entity<Skill>().ToTable("Skills");
            modelBuilder.Entity<LibraryResource>().ToTable("LibraryResources");

            Seed(modelBuilder);
        }

        private void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Intern>().HasData(
                new Intern
                {
                    Id = 1,
                    Name = "Fineas Gavre",
                    Birthdate = new DateTime(2000, 8, 8),
                    Email = "fineasgavre@gmail.com",
                },
                new Intern
                {
                    Id = 2,
                    Name = "Andrei Popescu",
                    Birthdate = new DateTime(2001, 1, 1),
                    Email = "andreipopescu@gmail.com",
                });

            modelBuilder.Entity<Skill>().HasData(
                new Skill
                {
                    Id = 1,
                    Name = "Skill1",
                    Description = "Nice skill 1",
                    SkillMatrixUrl = "https://url.here",
                    InternId = 1,
                },
                new Skill
                {
                    Id = 2,
                    Name = "Skill2",
                    Description = "Nice skill 2",
                    SkillMatrixUrl = "https://url2.here",
                    InternId = 2,
                });

            modelBuilder.Entity<LibraryResource>().HasData(
                new LibraryResource
                {
                    Id = 1,
                    Name = "LibraryResource",
                    Recommendation = "Good",
                    URL = "https://libresources.com",
                    SkillId = 1,
                },
                new LibraryResource
                {
                    Id = 2,
                    Name = "LibraryResource2",
                    Recommendation = "Bad",
                    URL = "https://libresources2.com",
                    SkillId = 2,
                });
        }
    }
}
