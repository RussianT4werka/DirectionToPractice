using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DirectionToPractice.DB.Models
{
    public partial class Teacher
    {
        public int Id { get; set; }
        public string Surname { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Patronymic { get; set; } = null!;

        [NotMapped]
        public string FIO { get => Surname + " " + Name.First() + "." + Patronymic.First() + "."; }

        [NotMapped]
        public string SNP { get => Surname + " " + Name + " " + Patronymic; }
    }
}
