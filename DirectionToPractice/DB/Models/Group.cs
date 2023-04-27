using System;
using System.Collections.Generic;

namespace DirectionToPractice.DB.Models
{
    public partial class Group
    {
        public Group()
        {
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public int Number { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
