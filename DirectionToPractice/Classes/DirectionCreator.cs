using Cyriller;
using DirectionToPractice.DB.Models;
using Spire.Doc;
using Spire.Doc.Documents;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DirectionToPractice.Classes
{
    public class DirectionCreator
    {
        private static CyrName cyrName = new CyrName();
        private static CyrNumber cyrNumber = new CyrNumber();

        public static void GetDirections(Student student, Practice practice)
        {
            //Загружаем документ
            Document document = new Document();
            document.LoadFromFile("Направление на Практику.docx");
            Section section = document.Sections[0];

            //Выбираем 5 абзац(Таблица в счёт не идёт)
            Paragraph paraFIO = section.Paragraphs[5];

            CyrResult resultFIO = cyrName.Decline($"{student.Surname}", $"{student.Name}", $"{student.Patronymic}");

            paraFIO.Replace("Лукошкина_______Анатолия___________Алексеевича", $"{resultFIO.Родительный}", false, true);

            //Выбираем 7 абзац
            Paragraph paraCCS = section.Paragraphs[7];
            paraCCS.Replace("3", $"{student.Group.Course}", false, true);
            paraCCS.Replace("09.02.02", $"{student.Group.Speciality.Code}", false, true);
            paraCCS.Replace("Компьютерные сети", $"{student.Group.Speciality.Name}", false, true);

            //Выбираем 8 абзац
            Paragraph paraSP = section.Paragraphs[8];
            paraSP.Replace("производственной", $"{practice.PracticeType}", false, true);

            //Выбираем 10 абзац
            Paragraph paraMDDC = section.Paragraphs[10];
            if (practice.PracticeType == "преддипломной")
            {
                paraMDDC.Replace("ПП.04 «Выполнение работ по профессии «Наладчик технологического оборудования»» ", "", false, true);
                Paragraph paraH = section.Paragraphs[10];
                string result = cyrNumber.Case(practice.CountHours.Value, "час", "часа", "часов");
                paraH.Replace("252 часа", $"{practice.CountHours} {result}", false, true);
                paraH.Replace("01.05.2023", $"{practice.DateStart.Value.Date.ToString("d")}", false, true);
                paraH.Replace("17.06.2023", $"{practice.DateEnd.Value.Date.ToString("d")}", false, true);
            }
            else
            {
                paraMDDC.Replace("ПП.04 «Выполнение работ по профессии «Наладчик технологического оборудования»»", $"{practice.ModulePractice}", false, true);
                paraMDDC.Replace("01.05.2023", $"{practice.DateStart.Value.Date.ToString("d")}", false, true);
                paraMDDC.Replace("17.06.2023", $"{practice.DateEnd.Value.Date.ToString("d")}", false, true);

                string result = cyrNumber.Case(practice.CountHours.Value, "час", "часа", "часов");
                paraMDDC.Replace("252 часа", $"{practice.CountHours} {result}", false, true);
            }
            //Выбираем 12 абзац
            Paragraph paraSCS = section.Paragraphs[12];
            paraSCS.Replace("ИП Суроп А. В.", $"{practice.Organisation}", false, true);
            paraSCS.Replace("г.Владивосток", $"{practice.City}", false, true);
            paraSCS.Replace("Камская 1/2", $"{practice.StreetHouse}", false, true);

            //Выбираем 14 абзац
            Paragraph para = section.Paragraphs[14];
            var date = practice.DateStart.Value.Date.ToString("D");
            date = date.Insert(0, "\"");

            if (practice.DateStart.Value.Day < 10)
            {
                date = date.Insert(1, "0");
            }

            date = date.Insert(3, "\"");

            para.Replace("\"01\" мая 2023 г.", date, false, true);

            date = practice.DateEnd.Value.Date.ToString("D");

            date = date.Insert(0, "\"");

            if (practice.DateEnd.Value.Day < 10)
            {
                date = date.Insert(1, "0");
            }

            date = date.Insert(3, "\"");

            para.Replace("\"17\" июня 2023 г.", date, false, true);

            //Выбираем 70 абзац...
            Paragraph paraSPFIO = section.Paragraphs[70];
            paraSPFIO.Replace("Михайлюк П.К", $"{practice.Teacher.FIO}", false, true);

            try
            {
                string file = $"Направление на Практику {student.FIO} гр.{student.Group.Number}.docx";
                document.SaveToFile(file, FileFormat.Docx);
                ProcessStartInfo process = new ProcessStartInfo();
                process.FileName = "explorer.exe";
                process.Arguments = file;
                process.UseShellExecute = true;
                System.Diagnostics.Process.Start(process);
            }
            catch
            {
                MessageBox.Show("Закройте предыдущий файл");
            }

        }
    }
}
