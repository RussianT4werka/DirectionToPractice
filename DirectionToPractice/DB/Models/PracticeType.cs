using System;
using System.Collections.Generic;

namespace DirectionToPractice.DB.Models
{
    public partial class PracticeType
    {
        public PracticeType()
        {
            ModulePractices = new HashSet<ModulePractice>();
            Practices = new HashSet<Practice>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<ModulePractice> ModulePractices { get; set; }
        public virtual ICollection<Practice> Practices { get; set; }
    }
}
