using System.Collections.Concurrent;
using System.Collections.Generic;

namespace HelloWorldWebApp.Data
{
    public interface ITeamMemberStore
    {
        IList<string> GetTeamMembers();
        void AddTeamMember(string teamMemberName);
    }
}