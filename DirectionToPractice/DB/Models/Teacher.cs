using System;
using System.Collections.Generic;

namespace DirectionToPractice.DB.Models
{
    public partial class Teacher
    {
        public int Id { get; set; }
        public string Surname { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Patronymic { get; set; } = null!;
    }
}
