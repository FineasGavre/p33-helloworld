using HelloWorldWebApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HelloWorldTests
{
    public class PostgresConnectionStringConverterTests
    {
        [Fact]
        public void CheckForProperConversion()
        {
            // Assume
            var connectionUri = "postgres://fineasgavre:123456@helloworld.com:5432/testdb";
            
            // Act            
            var connectionString = PostgresConnectionStringConverter.ConvertPostgresUriToNpgsqlConnectionString(connectionUri);

            // Assert
            Assert.Equal("Host=helloworld.com;Port=5432;Username=fineasgavre;Password=123456;Database=testdb;SSL Mode=Require;Trust Server Certificate=true", connectionString);
        }
    }
}
