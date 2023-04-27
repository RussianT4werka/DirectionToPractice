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
        private Page currentPage;
        public Command ListGroups { get; set; }
        public Command ListStudents { get; set; }
        public Page CurrentPage 
        { 
            get => currentPage;
            set
            {
                currentPage = value;
                SignalChanged();
            }
        }
        public MainWindowVM()
        {
            CurrentPage = new ListGroupPage(this);

            ListGroups = new Command(() =>
            {
                CurrentPage = new ListGroupPage(this);
            });

            ListStudents = new Command(() =>
            {
                CurrentPage = new ListAllStudentPage();
            });
        }
    }
}
