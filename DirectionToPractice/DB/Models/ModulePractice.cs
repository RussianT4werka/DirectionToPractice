﻿using System;
using System.Collections.Generic;

namespace DirectionToPractice.DB.Models
{
    public partial class ModulePractice
    {
        public ModulePractice()
        {
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string Number { get; set; } = null!;
        public string Text { get; set; } = null!;
        public int SpecialityId { get; set; }
        public int PracticeTypeId { get; set; }

        public virtual PracticeType PracticeType { get; set; } = null!;
        public virtual Speciality Speciality { get; set; } = null!;
        public virtual ICollection<Student> Students { get; set; }
    }
}
