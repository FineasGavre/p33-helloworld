using HelloWorldWebApp.Data.Models;
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
            // Assume
            var intern = new Intern
            {
                Birthdate = new DateTime(2000, 8, 8)
            };

            // Act
            var age = intern.GetAge();

            // Assert
            Assert.Equal(21, age);
        }
    }
}
