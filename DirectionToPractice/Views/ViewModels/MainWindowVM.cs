using DirectionToPractice.DB.Models;
using DirectionToPractice.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DirectionToPractice.Views.ViewModels
{
    public class MainWindowVM : BaseVM
    {
        private Stack<Page> pages = new();
        private Page currentPage;
        public Command ListGroups { get; set; }
        public Command ListStudents { get; set; }
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
            SetPage(new ListGroupPage(this));

            ListGroups = new Command(() =>
            {
                SetPage(new ListGroupPage(this));
            });

            ListStudents = new Command(() =>
            {
                SetPage(new ListAllStudentPage(this));
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
