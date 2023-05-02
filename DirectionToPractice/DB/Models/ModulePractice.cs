using System;
using System.Collections.Generic;

namespace DirectionToPractice.DB.Models
{
    public partial class ModulePractice
    {
        public int Id { get; set; }
        public string Number { get; set; } = null!;
        public string Text { get; set; } = null!;
        public int SpecialityId { get; set; }

        public virtual Speciality Speciality { get; set; } = null!;
    }
}
