using Cyriller;
using DirectionToPractice.Classes;
using DirectionToPractice.DB;
using DirectionToPractice.DB.Models;
using DirectionToPractice.Tools;
using Microsoft.EntityFrameworkCore;
using Spire.Doc;
using Spire.Doc.Documents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DirectionToPractice.Views.ViewModels
{
    public class ListAllStudentPageVM : BaseVM
    {
        
        private string search;
        private ObservableCollection<Student> students;
        private Practice practice;
        private Speciality speciality;

        public Command CreateDirections { get; set; }
        public ObservableCollection<Student> Students
        {
            get => students;
            set
            {
                students = value;
                SignalChanged();
            }
        }

        public string Search
        {
            get => search;
            set
            {
                search = value;
                DoSearch();
            }

        }

        public Practice Practice
        {
            get => practice;
            set
            {
                practice = value;
                SignalChanged();
            }
        }

        public Speciality Speciality 
        { 
            get => speciality;
            set
            {
                speciality = value;
                SignalChanged();
            }
        }

        public List<StudentPractice> StudentPractices { get; set; }

        public ListAllStudentPageVM(Practice practice, MainWindowVM mainVM, Speciality? selectedSpeciality)
        {
            this.Speciality = selectedSpeciality;
            Practice = practice;
            Students = new ObservableCollection<Student>(practiceContext.GetInstance().Students.Include(s => s.Group).ThenInclude(s => s.Speciality).Where(s => s.Group.SpecialityId == Speciality.Id).ToList());
            CreateDirections = new Command(() =>
            {
                if (Practice.Id != 0)
                    return;

                StudentPractices = new List<StudentPractice>();
                practiceContext.GetInstance().Practices.Add(Practice);
                practiceContext.GetInstance().SaveChanges();
                Practice = practiceContext.GetInstance().Practices.ToList().Last();
                foreach (var student in Students)
                {
                    if (student.Select)
                    {
                        StudentPractices.Add(new StudentPractice { PracticeId = Practice.Id, StudentId = student.Id, Practice = Practice, Student = student });
                        DirectionCreator.GetDirections(student, practice);
                        student.Select = false;
                        SignalChanged(nameof(student.Select));
                    }
                }
                practiceContext.GetInstance().StudentPractices.AddRange(StudentPractices);
                practiceContext.GetInstance().SaveChanges();


                mainVM.SetPage(new ListCreatedDirectionPage(mainVM));
            });
        }

        

        private void DoSearch()
        {
            IQueryable<Student> searchRequest = practiceContext.GetInstance().Students;

            if (!string.IsNullOrEmpty(Search))
                searchRequest = searchRequest.Where(s => s.Surname.Contains(Search) || s.Name.Contains(Search) || s.Patronymic.Contains(Search) || s.GroupNumber.ToString().Contains(Search));
            Students = new ObservableCollection<Student>(searchRequest);
        }
    }
}
