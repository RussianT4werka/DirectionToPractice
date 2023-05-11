﻿using System;
using System.Collections.Generic;

namespace DirectionToPractice.DB.Models
{
    public partial class Student
    {
        public Student()
        {
            StudentPractices = new HashSet<StudentPractice>();
        }

        public int Id { get; set; }
        public string Surname { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Patronymic { get; set; }
        public int GroupNumber { get; set; }

        public virtual Group Group { get; set; } = null!;
        public virtual ICollection<StudentPractice> StudentPractices { get; set; }
    }
}
