using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectionToPractice.DB.Models
{
    public partial class Student
    {
        private bool select;
        [NotMapped]
        public string FIO { get => Surname + " " + Name + " " + Patronymic; }

        [NotMapped]
        public bool Select
        {
            get => select;
            set => select = value;
        }
    }
}
