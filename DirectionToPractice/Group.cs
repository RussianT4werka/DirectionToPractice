using DirectionToPractice.DB.Models;
using System;
using System.Collections.Generic;

namespace DirectionToPractice
{
    public partial class Group
    {
        public Group()
        {
            Students = new HashSet<Student>();
        }

        public int Number { get; set; }
        public int SpecialityId { get; set; }
        public int Course { get; set; }

        public virtual Speciality Speciality { get; set; } = null!;
        public virtual ICollection<Student> Students { get; set; }
    }
}
