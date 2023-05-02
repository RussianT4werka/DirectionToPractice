using DirectionToPractice.Tools;
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

namespace DirectionToPractice.Views.ViewModels
{
    public class CreateDirectionPageVM: BaseVM
    {
        private static CyrName cyrName = new CyrName();

        public string SpeciesPractice { get; set; }
        public string ModulePractice { get; set; }
        public string Profession { get; set; }
        public DateTime DateStart { get; set; } = DateTime.Now;
        public DateTime DateEnd { get; set; } = DateTime.Now;
        public string CountHours { get; set; }
        public string SpeciesOrganisation { get; set; }
        public string City { get; set; }
        public string StreetHouse { get; set; }
        public string SupervisorPracticeFIO { get; set; }
        public Command CreateDirection { get; set; }
        public CreateDirectionPageVM(DB.Models.Student selectedStudent, MainWindowVM mainVM)
        {

            CreateDirection = new Command(() =>
            {
                //Загружаем документ
                Document document = new Document();
                document.LoadFromFile("Направление на Практику.docx");
                Section section = document.Sections[0];

                //Выбираем 5 абзац(Таблица в счёт не идёт)
                Paragraph paraFIO = section.Paragraphs[5];

                CyrResult resultSurname = cyrName.Decline($"{selectedStudent.Surname}");
                CyrResult resultName = cyrName.Decline($"{selectedStudent.Name}");
                CyrResult resultPatronymic = cyrName.Decline($"{selectedStudent.Patronymic}");

                paraFIO.Replace("Лукошкина", $"{resultSurname.Genitive}", false, true);
                paraFIO.Replace("Анатолия", $"{resultName.Genitive}", false, true);
                paraFIO.Replace("Алексеевича", $"{resultPatronymic.Genitive}", false, true);

                //Выбираем 7 абзац
                Paragraph paraCCS = section.Paragraphs[7];
                paraCCS.Replace("3", $"{selectedStudent.Group.Course}", false, true);
                paraCCS.Replace("09.02.02", $"{selectedStudent.Group.Speciality.Code}", false, true);
                paraCCS.Replace("Компьютерные сети", $"{selectedStudent.Group.Speciality.Name}", false, true);

                //Выбираем 5 абзац
                Paragraph paraSP = section.Paragraphs[8];
                paraSP.Replace("производственной", $"{SpeciesPractice}", false, true);

                //Выбираем 10 абзац
                Paragraph paraMDDC = section.Paragraphs[10];
                paraMDDC.Replace("ПП.04", $"{ModulePractice}", false, true);
                paraMDDC.Replace("«Выполнение работ по профессии «Наладчик технологического оборудования»»", $"{Profession}", false, true);
                paraMDDC.Replace("01.05.2023", $"{DateStart.Date.ToString("d")}", false, true);
                paraMDDC.Replace("17.06.2023", $"{DateEnd.Date.ToString("d")}", false, true);
                paraMDDC.Replace("252 часа", $"{CountHours}", false, true);

                //Выбираем 12 абзац
                Paragraph paraSCS = section.Paragraphs[12];
                paraSCS.Replace("ИП Суроп А. В.", $"{SpeciesOrganisation}", false, true);
                paraSCS.Replace("г.Владивосток", $"{City}", false, true);
                paraSCS.Replace("Камская 1/2", $"{StreetHouse}", false, true);

                //Выбираем 14 абзац
                Paragraph para = section.Paragraphs[14];
                var date = DateStart.Date.ToString("D");
                date = date.Insert(0, "\"");

                if (DateStart.Day < 10)
                {
                    date = date.Insert(1, "0");
                }

                date = date.Insert(3, "\"");

                para.Replace("\"01\" мая 2023 г.", date, false, true);

                date = DateEnd.Date.ToString("D");

                date = date.Insert(0, "\"");

                if (DateEnd.Day < 10)
                {
                    date = date.Insert(1, "0");
                }

                date = date.Insert(3, "\"");

                para.Replace("\"17\" июня 2023 г.", date, false, true);

                //Выбираем 70 абзац...
                Paragraph paraSPFIO = section.Paragraphs[70];
                paraSPFIO.Replace("Михайлюк П.К", $"{SupervisorPracticeFIO}", false, true);

                try
                {
                    document.SaveToFile($"Направление на Практику {selectedStudent.FIO} гр.{selectedStudent.Group.Number}.docx", FileFormat.Docx);
                    ProcessStartInfo process = new ProcessStartInfo();
                    process.FileName = "explorer.exe";
                    process.Arguments = $"Направление на Практику {selectedStudent.FIO} гр.{selectedStudent.Group.Number}.docx";
                    process.UseShellExecute = true;
                    System.Diagnostics.Process.Start(process);
                    mainVM.GoToPrevPage();
                }
                catch
                {
                    MessageBox.Show("Закройте предыдущий файл");
                }
                
            });
        }
    }
}
