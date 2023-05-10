using DirectionToPractice.DB;
using DirectionToPractice.DB.Models;
using DirectionToPractice.Tools;
using Microsoft.Win32;
using Spire.Doc;
using Spire.Doc.Documents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DirectionToPractice.Views.ViewModels
{
    public class UpdateBDPageVM: BaseVM
    {
        public Command UploadListStudent { get; set; }
        public Command DeleteListStudent { get; set; }
        public List<Student> Students { get; set; }
        public UpdateBDPageVM()
        {
            Students = new List<Student>(practiceContext.GetInstance().Students.ToList());
            UploadListStudent = new Command(() =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Word файлы|*.docx";
                if (openFileDialog.ShowDialog() == true)
                {
                    string fileName = openFileDialog.FileName;
                    MessageBox.Show("Выбран файл: " + openFileDialog.FileName);
                    Document doc = new Document();
                    doc.LoadFromFile($"{openFileDialog.FileName}", FileFormat.Docx2013);
                    StringBuilder sb = new StringBuilder();
                    //Extract text from Word and save to StringBuilder instance
                    foreach (Section section in doc.Sections)
                    {
                        foreach (Paragraph paragraph in section.Paragraphs)
                        {
                            var surname = paragraph.Text.Split(' ')[0];
                            var name = paragraph.Text.Split(' ')[1];
                            var patronymic = paragraph.Text.Split(' ')[2];
                            var groupNumber = paragraph.Text.Split(' ')[3];
                            Students.Add(new Student { Surname = surname, Name = name, Patronymic = patronymic, GroupNumber = int.Parse(groupNumber)});
                        }
                    }

                    practiceContext.GetInstance().Students.AddRange(Students);
                    practiceContext.GetInstance().SaveChanges();
                }
            });

            DeleteListStudent = new Command(() =>
            {

            });
        }
    }
}
