using System;
using System.Collections.Generic;

namespace DirectionToPractice.DB.Models
{
    public partial class Practice
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int CountHours { get; set; }
        public byte[]? Direction { get; set; }

        public virtual Student Student { get; set; } = null!;
    }
}
