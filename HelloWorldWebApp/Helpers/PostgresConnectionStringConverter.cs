using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldWebApp.Helpers
{
    /// <summary>
    /// Static helper class for Postgres Connection String conversion.
    /// </summary>
    public static class PostgresConnectionStringConverter
    {
        /// <summary>
        /// Converts a Postgres URI (starting with postgres://) to a Npgsql-compatible ConnectionString.
        /// </summary>
        /// <param name="postgresUri">String with Postgres URI.</param>
        /// <returns>String with Npgsql ConnectionString.</returns>
        public static string ConvertPostgresUriToNpgsqlConnectionString(string postgresUri)
        {
            postgresUri = postgresUri.Replace("postgres://", string.Empty);

            var pgUserPass = postgresUri.Split("@")[0];
            var pgHostPortDb = postgresUri.Split("@")[1];
            var pgHostPort = pgHostPortDb.Split("/")[0];
            var pgDb = pgHostPortDb.Split("/")[1];
            var pgUser = pgUserPass.Split(":")[0];
            var pgPass = pgUserPass.Split(":")[1];
            var pgHost = pgHostPort.Split(":")[0];
            var pgPort = pgHostPort.Split(":")[1];

            return $"Host={pgHost};Port={pgPort};Username={pgUser};Password={pgPass};Database={pgDb};SSL Mode=Require;Trust Server Certificate=true";
        }
    }
}
