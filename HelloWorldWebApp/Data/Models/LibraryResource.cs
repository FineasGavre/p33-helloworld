using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldWebApp.Data.Models
{
    public class LibraryResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Recommendation { get; set; }
        public string URL { get; set; }

        public Skill Skill { get; set; }
        public int SkillId { get; set; }
    }
}
