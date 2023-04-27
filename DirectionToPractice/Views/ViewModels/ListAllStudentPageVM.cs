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
    public class ListAllStudentPageVM : BaseVM
    {
        private string search;
        private ObservableCollection<Student> students;
        private Student selectedStudent;

        public Command CreateDirection { get; set; }
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
        public ListAllStudentPageVM()
        {
            Students = new ObservableCollection<Student>(practiceContext.GetInstance().Students.ToList());
            CreateDirection = new Command(() =>
            {
                try
                {
                    var helper = new WordHelper("Test.docx");
                    var items = new Dictionary<string, string>
                {
                    {"<FIO>", SelectedStudent.Surname }
                };

                    helper.EditDocument(items);
                }
                catch
                {
                    MessageBox.Show("У вас установлена неофициальная версия Word");
                }
                
            });
        }

        private void DoSearch()
        {
            IQueryable<Student> searchRequest = practiceContext.GetInstance().Students;

            if (!string.IsNullOrEmpty(Search))
                searchRequest = searchRequest.Where(s => s.Surname.Contains(Search) || s.Name.Contains(Search) || s.Patronymic.Contains(Search));
            Students = new ObservableCollection<Student>(searchRequest);
        }
    }
}
