using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectionToPractice.DB.Models
{
    public partial class Teacher
    {
        [NotMapped]
        public string FIO { get => Surname + " " + Name.First() + "." + Patronymic.First() + "."; }

        [NotMapped]
        public string SNP { get => Surname + " " + Name + " " + Patronymic; }
    }
}
