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
                //Load Document
                Document document = new Document();
                document.LoadFromFile("Test.docx");
                Section section = document.Sections[0];
                Section section1 = document.Sections[0];

                Paragraph para2 = section1.Paragraphs[1];
                para2.Replace("___________________________________", $"{SelectedStudent.FIO}", false, true);
                //TextRange tr = para2.AppendText($"{SelectedStudent.Surname}");

                document.SaveToFile("Direction.docx", FileFormat.Docx);
                ProcessStartInfo process = new ProcessStartInfo();
                process.FileName = "explorer.exe";
                process.Arguments = "Direction.docx";
                process.UseShellExecute = true;
                System.Diagnostics.Process.Start(process);
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
