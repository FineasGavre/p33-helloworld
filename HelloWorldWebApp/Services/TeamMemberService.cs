using HelloWorldWebApp.Data;
using HelloWorldWebApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldWebApp
{
    public class TeamMemberService : ITeamMemberService
    {
        private readonly ApplicationContext context;

        public TeamMemberService(ApplicationContext context)
        {
            this.context = context;
        }

        public IList<TeamMember> GetTeamMembers()
        {
            return context.TeamMembers.ToList();
        }

        public void AddTeamMember(TeamMember teamMember)
        {
            context.TeamMembers.Add(teamMember);
            context.SaveChanges();
        }

        public void UpdateTeamMember(TeamMember teamMember)
        {
            context.TeamMembers.Update(teamMember);
            context.SaveChanges();
        }

        public void DeleteTeamMember(int id)
        {
            context.TeamMembers.Remove(context.TeamMembers.Find(id));
            context.SaveChanges();
        }
    }
}
