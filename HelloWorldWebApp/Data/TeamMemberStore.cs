using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldWebApp.Data
{
    public class TeamMemberStore : ITeamMemberStore
    {
        private BlockingCollection<string> teamMembers = new BlockingCollection<string>();

        public IList<string> GetTeamMembers() => teamMembers.ToList<string>();

        public void AddTeamMember(string teamMemberName)
        {
            teamMembers.Add(teamMemberName);
        }
    }
}
