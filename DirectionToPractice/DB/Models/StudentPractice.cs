using System;
using System.Collections.Generic;

namespace DirectionToPractice.DB.Models
{
    public partial class StudentPractice
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int PracticeId { get; set; }

        public virtual Practice Practice { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
    }
}
