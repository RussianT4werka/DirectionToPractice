using DirectionToPractice.DB;
using DirectionToPractice.DB.Models;
using DirectionToPractice.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DirectionToPractice.Views.ViewModels
{
    public class ListCreatedDirectionPageVM: BaseVM
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
        public ListCreatedDirectionPageVM(MainWindowVM mainVM)
        {
            Students = new ObservableCollection<Student>(practiceContext.GetInstance().Students.ToList());

            ToCreateDirectionPage = new Command(() =>
            {
                if (SelectedStudent == null)
                {
                    MessageBox.Show("Вы не выбрали студента");
                    return;
                }
                else
                {
                    mainVM.SetPage(new CreateDirectionPage(SelectedStudent, mainVM));
                }
            });
        }
    }
}
