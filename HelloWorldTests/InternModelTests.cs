using HelloWorldWebApp.Data.Models;
using Pose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HelloWorldTests
{
    public class InternModelTests
    {
        public InternModelTests()
        {
        }

        [Fact]
        public void CheckIfAgeIsCorrect()
        {
            var dateTimeShim = Shim.Replace(() => DateTime.Now).With(() => new DateTime(2021, 08, 12));

            PoseContext.Isolate(() =>
            {
                // Arrange
                var intern = new Intern
                {
                    Birthdate = new DateTime(2000, 8, 8)
                };

                // Act
                var age = intern.GetAge();

                // Assert
                Assert.Equal(21, age);
            });
        }
    }
}
