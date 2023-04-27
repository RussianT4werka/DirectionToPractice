using System;
using System.Collections.Generic;

namespace DirectionToPractice.DB.Models
{
    public partial class Student
    {
        public Student()
        {
            Practices = new HashSet<Practice>();
        }

        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Surname { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Patronymic { get; set; }

        public virtual Group Group { get; set; } = null!;
        public virtual ICollection<Practice> Practices { get; set; }
    }
}
