using DirectionToPractice.DB;
using DirectionToPractice.DB.Models;
using DirectionToPractice.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectionToPractice.Views.ViewModels
{
    public class ListStudentPageVM: BaseVM
    {
        public List<Student> Students { get; set; }
        public ListStudentPageVM(DB.Models.Group selectedGroup)
        {
            Students = new List<Student>(practiceContext.GetInstance().Students.Where( s => s.GroupId == selectedGroup.Id));
        }
    }
}
