using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DirectionToPractice.DB.Models
{
    public partial class Speciality
    {
        public Speciality()
        {
            Groups = new HashSet<Group>();
            ModulePractices = new HashSet<ModulePractice>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;

        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<ModulePractice> ModulePractices { get; set; }

        [NotMapped]
        public string FullName { get => Code + " " + Name; }
    }
}
