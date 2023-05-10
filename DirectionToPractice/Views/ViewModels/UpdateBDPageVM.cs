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
        public List<StudentPractice> StudentPractices { get; set; }
        public List<Student> Students { get; set; }
        public List<Practice> Practices { get; set; }
        public Student Student { get; set; }
        public UpdateBDPageVM()
        {
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
                            try
                            {
                                var surname = paragraph.Text.Split(' ')[0];
                                var name = paragraph.Text.Split(' ')[1];
                                var patronymic = paragraph.Text.Split(' ')[2];
                                var groupNumber = paragraph.Text.Split(' ')[3];
                                Student = new Student { Surname = surname, Name = name, Patronymic = patronymic, GroupNumber = int.Parse(groupNumber) };
                                practiceContext.GetInstance().Students.AddRange(Student);
                                practiceContext.GetInstance().SaveChanges();
                            }
                            catch
                            {
                                MessageBox.Show("Что-то пошло не так. Возможно неподходящий формат документа.");
                                return;
                            }
                            
                        }
                    }
                    MessageBox.Show("Студенты из списка успешно добавлены");
                }
            });

            DeleteListStudent = new Command(() =>
            {
                StudentPractices = new List<StudentPractice>(practiceContext.GetInstance().StudentPractices.ToList());
                Students = new List<Student>(practiceContext.GetInstance().Students.ToList());
                Practices = new List<Practice>(practiceContext.GetInstance().Practices.ToList());
                try
                {
                    practiceContext.GetInstance().StudentPractices.RemoveRange(StudentPractices);
                    practiceContext.GetInstance().SaveChanges();

                    practiceContext.GetInstance().Students.RemoveRange(Students);
                    practiceContext.GetInstance().SaveChanges();

                    practiceContext.GetInstance().RemoveRange(Practices);
                    practiceContext.GetInstance().SaveChanges();

                    MessageBox.Show("Все студенты успешно удалены");
                    return;
                }
                catch
                {
                    MessageBox.Show("Что-то пошло не так");
                    return;
                }
                
            });
        }
    }
}
