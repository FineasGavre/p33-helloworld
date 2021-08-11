using HelloWorldWebApp;
using HelloWorldWebApp.Data;
using HelloWorldWebApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Xunit;

namespace HelloWorldTests
{
    public class TeamMemberServiceTests
    {
        public TeamMemberServiceTests()
        {
            ContextOptions = new DbContextOptionsBuilder<ApplicationContext>()
                .UseSqlite("Filename=test.db")
                .Options;

            Seed();
        }

        public DbContextOptions<ApplicationContext> ContextOptions { get; }

        private void Seed()
        {
            using (var context = new ApplicationContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var one = new TeamMember
                {
                    Name = "TeamMember1"
                };

                var two = new TeamMember
                {
                    Name = "TeamMember2"
                };

                var three = new TeamMember
                {
                    Name = "TeamMember3"
                };

                var four = new TeamMember
                {
                    Name = "TeamMember4"
                };

                context.AddRange(one, two, three, four);
                context.SaveChanges();
            }
        }

        [Fact]
        public void CheckTeamMemberCount()
        {
            using var context = new ApplicationContext(ContextOptions);

            // Assume
            var teamService = new TeamMemberService(context);

            // Act
            var teamMembers = teamService.GetTeamMembers();

            // Assert
            Assert.Equal(4, teamMembers.Count);
        }

        [Fact]
        public void DeleteTeamMember()
        {
            using var context = new ApplicationContext(ContextOptions);

            // Assume
            var teamService = new TeamMemberService(context);
            var expectedCount = teamService.GetTeamMembers().Count - 1;
            var teamMember = teamService.GetTeamMembers()[0];

            // Act
            teamService.DeleteTeamMember(teamMember.Id);

            // Assert
            Assert.Equal(expectedCount, teamService.GetTeamMembers().Count);
            Assert.DoesNotContain(teamMember, teamService.GetTeamMembers());
        }

        [Fact]
        public void AddTeamMember()
        {
            using var context = new ApplicationContext(ContextOptions);

            // Assume
            var teamService = new TeamMemberService(context);
            var expectedCount = teamService.GetTeamMembers().Count + 1;
            var teamMember = new TeamMember
            {
                Name = "TestMember"
            };

            // Act
            teamService.AddTeamMember(teamMember);

            // Assert
            Assert.Equal(expectedCount, teamService.GetTeamMembers().Count);
            Assert.Contains(teamMember, teamService.GetTeamMembers());
        }

        [Fact]
        public void UpdateTeamMember()
        {
            using var context = new ApplicationContext(ContextOptions);

            // Assume
            var teamService = new TeamMemberService(context);
            var teamMember = teamService.GetTeamMembers()[0];


            // Act
            teamMember.Name = "NewName";
            teamService.UpdateTeamMember(teamMember);

            // Assert
            var updatedTeamMember = teamService.GetTeamMembers().ToList().Find(member => member.Id == teamMember.Id);

            Assert.Equal("NewName", updatedTeamMember.Name);
        }
    }
}
