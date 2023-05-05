﻿using DirectionToPractice.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spire.Doc;
using Spire.Doc.Documents;
using System.Windows;
using Cyriller;
using DirectionToPractice.DB.Models;
using DirectionToPractice.DB;
using Microsoft.EntityFrameworkCore;

namespace DirectionToPractice.Views.ViewModels
{
    public class CreateDirectionPageVM : BaseVM
    {
        private static CyrName cyrName = new CyrName();
        private static CyrNumber cyrNumber = new CyrNumber();
        private bool block = true;
        private PracticeType selectedPracticeType;
        private List<ModulePractice> modulePractices;
        private Practice practice;

        public Student SelectedStudent { get; set; }

        public bool Block
        {
            get => block;
            set
            {
                block = value;
                SignalChanged();
            }
        }

        public List<PracticeType> PracticeTypes { get; set; }
        public PracticeType SelectedPracticeType
        {
            get => selectedPracticeType;
            set
            {
                selectedPracticeType = value;
                Filter();
                SignalChanged();
            }
        }

        public List<ModulePractice> ModulePractices
        {
            get => modulePractices;
            set
            {
                modulePractices = value;
                SignalChanged();
            }
        }
        public ModulePractice SelectedModulePractice { get; set; }

        public List<Teacher> Teachers { get; set; }
        public Teacher SelectedTeacher { get; set; }

        public DateTime DateStart { get; set; } = DateTime.Now;
        public DateTime DateEnd { get; set; } = DateTime.Now;

        public int CountHours { get; set; }
        public string SpeciesOrganisation { get; set; }
        public string City { get; set; }
        public string StreetHouse { get; set; }

        public Practice Practice 
        {
            get => practice;
            set
            {
                practice = value;
                SignalChanged();
            }
        }

        public Command CreateDirection { get; set; }
        public CreateDirectionPageVM(MainWindowVM mainVM)
        {
            Filter();
            PracticeTypes = new List<PracticeType>(practiceContext.GetInstance().PracticeTypes.ToList());
            Teachers = new List<Teacher>(practiceContext.GetInstance().Teachers.ToList());
            CreateDirection = new Command(() =>
            {
                Practice = new Practice() { Organisation = SpeciesOrganisation, City = City, StreetHouse = StreetHouse, PracticeTypeId = SelectedPracticeType.Id, ModulePracticeId = SelectedModulePractice.Id, TeacherId = SelectedTeacher.Id, DateStart = DateStart, DateEnd = DateEnd, CountHours = CountHours};
                
                mainVM.SetPage(new ListAllStudentPage(Practice));

                //if (SelectedPracticeType == null || SelectedModulePractice == null || SelectedTeacher == null || string.IsNullOrEmpty(CountHours.ToString()) || string.IsNullOrEmpty(SpeciesOrganisation) || string.IsNullOrEmpty(City) || string.IsNullOrEmpty(StreetHouse))
                //{
                //    MessageBox.Show("Не все поля заполнены");
                //}
                //else
                //{
                //    //Загружаем документ
                //    Document document = new Document();
                //    document.LoadFromFile("Направление на Практику.docx");
                //    Section section = document.Sections[0];

                //    //Выбираем 5 абзац(Таблица в счёт не идёт)
                //    Paragraph paraFIO = section.Paragraphs[5];

                //    CyrResult resultSurname = cyrName.Decline($"{selectedStudent.Surname}");
                //    CyrResult resultName = cyrName.Decline($"{selectedStudent.Name}");
                //    CyrResult resultPatronymic = cyrName.Decline($"{selectedStudent.Patronymic}");

                //    paraFIO.Replace("Лукошкина", $"{resultSurname.Accusative}", false, true);
                //    paraFIO.Replace("Анатолия", $"{resultName.Accusative}", false, true);
                //    paraFIO.Replace("Алексеевича", $"{resultPatronymic.Accusative}", false, true);

                //    //Выбираем 7 абзац
                //    Paragraph paraCCS = section.Paragraphs[7];
                //    paraCCS.Replace("3", $"{selectedStudent.Group.Course}", false, true);
                //    paraCCS.Replace("09.02.02", $"{selectedStudent.Group.Speciality.Code}", false, true);
                //    paraCCS.Replace("Компьютерные сети", $"{selectedStudent.Group.Speciality.Name}", false, true);

                //    //Выбираем 8 абзац
                //    Paragraph paraSP = section.Paragraphs[8];
                //    paraSP.Replace("производственной", $"{SelectedPracticeType.Name}", false, true);

                //    //Выбираем 10 абзац
                //    Paragraph paraMDDC = section.Paragraphs[10];
                //    if (SelectedPracticeType.Id == 3)
                //    {
                //        paraMDDC.Replace("ПП.04 «Выполнение работ по профессии «Наладчик технологического оборудования»» ", "", false, true);
                //        Paragraph paraH = section.Paragraphs[10];
                //        string result = cyrNumber.Case(CountHours, "час", "часа", "часов");
                //        paraH.Replace("252 часа", $"{CountHours} {result}", false, true);
                //        paraH.Replace("01.05.2023", $"{DateStart.Date.ToString("d")}", false, true);
                //        paraH.Replace("17.06.2023", $"{DateEnd.Date.ToString("d")}", false, true);
                //    }
                //    else
                //    {
                //        switch (SelectedPracticeType.Id)
                //        {
                //            case 1:
                //                paraMDDC.Replace("ПП", "УП", false, true);
                //                break;
                //            case 2:
                //                paraMDDC.Replace("ПП", "ПП", false, true);
                //                break;
                //        }
                //        paraMDDC.Replace("04", $"{SelectedModulePractice.Number}", false, true);
                //        paraMDDC.Replace("Выполнение работ по профессии «Наладчик технологического оборудования»", $"{SelectedModulePractice.Text}", false, true);
                //        paraMDDC.Replace("01.05.2023", $"{DateStart.Date.ToString("d")}", false, true);
                //        paraMDDC.Replace("17.06.2023", $"{DateEnd.Date.ToString("d")}", false, true);

                //        string result = cyrNumber.Case(CountHours, "час", "часа", "часов");
                //        paraMDDC.Replace("252 часа", $"{CountHours} {result}", false, true);
                //    }
                //    //Выбираем 12 абзац
                //    Paragraph paraSCS = section.Paragraphs[12];
                //    paraSCS.Replace("ИП Суроп А. В.", $"{SpeciesOrganisation}", false, true);
                //    paraSCS.Replace("г.Владивосток", $"{City}", false, true);
                //    paraSCS.Replace("Камская 1/2", $"{StreetHouse}", false, true);

                //    //Выбираем 14 абзац
                //    Paragraph para = section.Paragraphs[14];
                //    var date = DateStart.Date.ToString("D");
                //    date = date.Insert(0, "\"");

                //    if (DateStart.Day < 10)
                //    {
                //        date = date.Insert(1, "0");
                //    }

                //    date = date.Insert(3, "\"");

                //    para.Replace("\"01\" мая 2023 г.", date, false, true);

                //    date = DateEnd.Date.ToString("D");

                //    date = date.Insert(0, "\"");

                //    if (DateEnd.Day < 10)
                //    {
                //        date = date.Insert(1, "0");
                //    }

                //    date = date.Insert(3, "\"");

                //    para.Replace("\"17\" июня 2023 г.", date, false, true);

                //    //Выбираем 70 абзац...
                //    Paragraph paraSPFIO = section.Paragraphs[70];
                //    paraSPFIO.Replace("Михайлюк П.К", $"{SelectedTeacher.FIO}", false, true);

                //    try
                //    {
                //        string file = $"Направление на Практику {selectedStudent.FIO} гр.{selectedStudent.Group.Number}.docx";
                //        document.SaveToFile(file, FileFormat.Docx);
                //        ProcessStartInfo process = new ProcessStartInfo();
                //        process.FileName = "explorer.exe";
                //        process.Arguments = file;
                //        process.UseShellExecute = true;
                //        System.Diagnostics.Process.Start(process);
                //        mainVM.GoToPrevPage();
                //    }
                //    catch
                //    {
                //        MessageBox.Show("Закройте предыдущий файл");
                //    }
                //}
            });
        }
        private void Filter()
        {
            IQueryable<ModulePractice> modulePractices = practiceContext.GetInstance().ModulePractices;
            if (SelectedPracticeType != null)
                modulePractices = modulePractices.Where(s => s.PracticeTypeId == SelectedPracticeType.Id);
            if (SelectedStudent != null)
                modulePractices = modulePractices.Where(s => s.SpecialityId == SelectedStudent.Group.SpecialityId);
            ModulePractices = modulePractices.ToList();
        }
    }
}
