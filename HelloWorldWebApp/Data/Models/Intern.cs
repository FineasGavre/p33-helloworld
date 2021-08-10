using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldWebApp.Data.Models
{
    public class Intern
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }

        public ICollection<Skill> Skills { get; set; }

        public int GetAge() { 
            var now = DateTime.Now;
            var age = now.Year - Birthdate.Year;

            if (Birthdate > now.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}
