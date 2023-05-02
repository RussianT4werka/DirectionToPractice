using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DirectionToPractice.DB.Models
{
    public partial class Student
    {
        public int Id { get; set; }
        public int GroupNumber { get; set; }
        public string Surname { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Patronymic { get; set; }

        public virtual Group Group { get; set; } = null!;

        [NotMapped]
        public string FIO { get => Surname + " " + Name + " " + Patronymic; }
    }
}
