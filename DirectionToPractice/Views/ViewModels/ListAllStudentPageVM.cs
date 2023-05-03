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
        private Student selectedStudent;

        public Command ToCreateDirectionPage { get; set; }
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

        public string Search
        {
            get => search;
            set
            {
                search = value;
                DoSearch();
            }

        }
        public ListAllStudentPageVM(MainWindowVM mainVM)
        {
            Students = new ObservableCollection<Student>(practiceContext.GetInstance().Students.ToList());
            ToCreateDirectionPage = new Command(() =>
            {
                mainVM.SetPage(new CreateDirectionPage(SelectedStudent, mainVM));
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
