using DirectionToPractice.DB.Models;
using DirectionToPractice.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DirectionToPractice.Views.ViewModels
{
    public class MainWindowVM : BaseVM
    {
        private Stack<Page> pages = new();
        private Page currentPage;
        public Command ListAllCreatedDirection { get; set; }
        public Command ListAllTeachers { get; set; }
        public Command CreateDirection { get; set; }
        public Command ToUpdateBDPage { get; set; }
        public Command Info { get; set; }
        public Page CurrentPage 
        { 
            get => currentPage;
            private set
            {
                currentPage = value;
                SignalChanged();
            }
        }

        public MainWindowVM()
        {
            SetPage(new CreateDirectionPage(this));

            CreateDirection = new Command(() =>
            {
                SetPage(new CreateDirectionPage(this));
            });

            ListAllCreatedDirection = new Command(() =>
            {
                SetPage(new ListCreatedDirectionPage(this));
            });

            ListAllTeachers = new Command(() =>
            {
                SetPage(new ListTeachers());
            });

            ToUpdateBDPage = new Command(() =>
            {
                SetPage(new UpdateBDPage());
            });

            Info = new Command(() =>
            {
                if (MessageBox.Show("Перед началом работы загрузите список студентов в приложение. Для этого нажмите на кнопку \"Обновить БД\" и далее загрузите список студентов в word документе, который обязательно должен соответствовать нужному формату. В этом приложении вы можете создавать по одному или сразу несколько направлений на практику за раз, введя необходимые общие данные. Вы хотите ознакомится с примером списка студентов, соответствующий нужному формату?",
                    "Информация",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    string file = "Пример списка студентов.docx";
                    ProcessStartInfo process = new ProcessStartInfo();
                    process.FileName = "explorer.exe";
                    process.Arguments = file;
                    process.UseShellExecute = true;
                    System.Diagnostics.Process.Start(process);
                }
                
            });
        }

        public void GoToPrevPage()
        {
            pages.Pop();
            CurrentPage = pages.Peek();
        }

        public void SetPage(Page page)
        {
            pages.Push(page);
            CurrentPage = page;
        }
    }
}
