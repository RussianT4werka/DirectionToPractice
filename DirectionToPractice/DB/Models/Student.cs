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
        public string? Organisation { get; set; }
        public string? City { get; set; }
        public string? StreetHouse { get; set; }
        public byte? CanCreateDirection { get; set; }
        public int? PracticeTypeId { get; set; }
        public int? ModulePracticeId { get; set; }
        public int? TeacherId { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public int? CountHours { get; set; }

        public virtual Group Group { get; set; } = null!;
        public virtual ModulePractice? ModulePractice { get; set; }
        public virtual PracticeType? PracticeType { get; set; }
        public virtual Teacher? Teacher { get; set; }

        [NotMapped]
        public string FIO { get => Surname + " " + Name + " " + Patronymic; }

        [NotMapped]
        public bool Select
        {
            get => CanCreateDirection == 1;
            set => CanCreateDirection = value ? (byte)1 : (byte)0;
        }
    }
}
