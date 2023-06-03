using DirectionToPractice.Classes;
using DirectionToPractice.DB;
using DirectionToPractice.DB.Models;
using DirectionToPractice.Tools;
using Microsoft.EntityFrameworkCore;
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
        private ObservableCollection<StudentPractice> studentPractices;
        private StudentPractice selectedStudentPractice1;

        public ObservableCollection<StudentPractice> StudentPractices
        {
            get => studentPractices;
            set
            {
                studentPractices = value;
                SignalChanged();
            }
        }

        public StudentPractice SelectedStudentPractice
        {
            get => selectedStudentPractice1;
            set
            {
                selectedStudentPractice1 = value;
                SignalChanged();
            }
        }
        public List<Speciality> Specialities { get; set; }
        public List<string> PracticeTypes { get; set; }
        public Practice Practice { get; set; }
        public List<Teacher> Teachers { get; set; }
        public Command OpenDocument { get; set; }
        public ListCreatedDirectionPageVM()
        {
            StudentPractices = new ObservableCollection<StudentPractice>(practiceContext.GetInstance().StudentPractices.Include(s => s.Student).Include(s => s.Practice).ToList());
            Specialities = practiceContext.GetInstance().Specialities.ToList();
            PracticeTypes = new List<string> { "учебной", "производственной", "преддипломной" };
            Teachers = practiceContext.GetInstance().Teachers.ToList();

            OpenDocument = new Command(() =>
            {
                if (SelectedStudentPractice.SelectedSpeciality != null && SelectedStudentPractice.Course >= 1 && SelectedStudentPractice.Course <= 4)
                {
                    DirectionCreator.GetDirections(SelectedStudentPractice.Student, SelectedStudentPractice.Practice, SelectedStudentPractice.SelectedSpeciality, SelectedStudentPractice.Course);
                }
                else
                {
                    MessageBox.Show("Не все поля заполнены!");
                }
            });
        }
    }
}
