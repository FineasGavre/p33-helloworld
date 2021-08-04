using HelloWorldWebApp.Data.Models;
using System.Collections.Generic;

namespace HelloWorldWebApp
{
    public interface ITeamMemberService
    {
        void AddTeamMember(TeamMember teamMember);
        IList<TeamMember> GetTeamMembers();
    }
}