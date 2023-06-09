﻿using System;
using System.Collections.Generic;

namespace DirectionToPractice.DB.Models
{
    public partial class Practice
    {
        public Practice()
        {
            StudentPractices = new HashSet<StudentPractice>();
        }

        public int Id { get; set; }
        public string? Organisation { get; set; }
        public string? City { get; set; }
        public string? StreetHouse { get; set; }
        public string? PracticeType { get; set; }
        public string? ModulePractice { get; set; }
        public int? TeacherId { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public int? CountHours { get; set; }

        public virtual Teacher? Teacher { get; set; }
        public virtual ICollection<StudentPractice> StudentPractices { get; set; }
    }
}
