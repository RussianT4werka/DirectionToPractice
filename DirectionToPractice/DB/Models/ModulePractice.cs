using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DirectionToPractice.DB.Models
{
    public partial class ModulePractice
    {
        public ModulePractice()
        {
            Practices = new HashSet<Practice>();
        }

        public int Id { get; set; }
        public string Number { get; set; } = null!;
        public string Text { get; set; } = null!;
        public int SpecialityId { get; set; }
        public int PracticeTypeId { get; set; }

        public virtual PracticeType PracticeType { get; set; } = null!;
        public virtual Speciality Speciality { get; set; } = null!;
        public virtual ICollection<Practice> Practices { get; set; }

        [NotMapped]
        public string FullName { get => Number + " " + Text; }
    }
}
