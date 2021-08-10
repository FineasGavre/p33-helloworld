using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldWebApp.Data.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SkillMatrixUrl { get; set; }
        public Intern Intern { get; set; }
        public int InternId { get; set; }

        public ICollection<LibraryResource> LibraryResources { get; set; }
    }
}
