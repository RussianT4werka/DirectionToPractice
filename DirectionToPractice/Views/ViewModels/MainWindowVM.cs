using DirectionToPractice.DB.Models;
using DirectionToPractice.Tools;
using System;
using System.Collections.Generic;
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

            ToUpdateBDPage = new Command(() =>
            {
                SetPage(new UpdateBDPage());
            });

            Info = new Command(() =>
            {
                MessageBox.Show("");
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
