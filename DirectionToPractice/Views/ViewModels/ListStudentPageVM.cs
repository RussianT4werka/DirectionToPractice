using DirectionToPractice.DB;
using DirectionToPractice.DB.Models;
using DirectionToPractice.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectionToPractice.Views.ViewModels
{
    public class ListStudentPageVM: BaseVM
    {
        private ObservableCollection<Student> students;
        private Student selectedStudent;
        public ObservableCollection<Student> Students
        {
            get => students;
            set
            {
                students = value;
                SignalChanged();
            }
        }

        public Student SelectedStudent
        {
            get => selectedStudent;
            set
            {
                selectedStudent = value;
                SignalChanged();
            }
        }
        public Command ToCreateDirectionPage { get; set; }
        public ListStudentPageVM(DB.Models.Group selectedGroup, MainWindowVM mainVM)
        {
            Students = new ObservableCollection<Student>(practiceContext.GetInstance().Students.Where( s => s.GroupNumber == selectedGroup.Number));

            ToCreateDirectionPage = new Command(() =>
            {
                mainVM.SetPage(new CreateDirectionPage(SelectedStudent, mainVM));
            });
        }
    }
}
