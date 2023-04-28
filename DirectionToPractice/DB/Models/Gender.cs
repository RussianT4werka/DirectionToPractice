using System;
using System.Collections.Generic;

namespace DirectionToPractice.DB.Models
{
    public partial class Gender
    {
        public Gender()
        {
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Student> Students { get; set; }
    }
}
