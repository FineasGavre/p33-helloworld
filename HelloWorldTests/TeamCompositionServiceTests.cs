using HelloWorldWebApp;
using HelloWorldWebApp.Data.Models;
using HelloWorldWebApp.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HelloWorldTests
{
    public class TeamCompositionServiceTests
    {
        private readonly Mock<ITeamMemberService> teamMemberService = new Mock<ITeamMemberService>();

        public TeamCompositionServiceTests()
        {
            teamMemberService.Setup(s => s.GetTeamMembers()).Returns(new List<TeamMember>
            {
                new TeamMember
                {
                    Id = 1,
                    Name = "Fineas"
                },
                new TeamMember
                {
                    Id = 3,
                    Name = "Andrei"
                },
                new TeamMember
                {
                    Id = 2,
                    Name = "Radu"
                }
            });
        }

        [Fact]
        public void CheckIfInitialsAreCorrect()
        {
            // Arrange
            var teamCompositionService = new TeamCompositionService(teamMemberService.Object);

            // Act
            var initials = teamCompositionService.GetInitialsOfTeam();

            // Assert
            Assert.Equal("FRA", initials);
        }
    }
}
