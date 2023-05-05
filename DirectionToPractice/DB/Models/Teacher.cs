using System;
using System.Collections.Generic;

namespace DirectionToPractice.DB.Models
{
    public partial class Teacher
    {
        public Teacher()
        {
            Practices = new HashSet<Practice>();
        }

        public int Id { get; set; }
        public string Surname { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Patronymic { get; set; } = null!;

        public virtual ICollection<Practice> Practices { get; set; }
    }
}
