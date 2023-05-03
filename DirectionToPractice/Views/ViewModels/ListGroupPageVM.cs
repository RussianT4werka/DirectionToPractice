using DirectionToPractice.DB;
using DirectionToPractice.DB.Models;
using DirectionToPractice.Tools;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DirectionToPractice.Views.ViewModels
{
    public class ListGroupPageVM : BaseVM
    {
        private List<Group> groups;
        private Group selectedGroup;

        public Command GetStudents { get; set; }

        public List<Group> Groups
        {
            get => groups;
            set
            {
                groups = value;
                SignalChanged();
            }
        }

        public Group SelectedGroup
        { 
            get => selectedGroup;
            set
            {
                selectedGroup = value;
                SignalChanged();
            }
        }
        public ListGroupPageVM(MainWindowVM mainVM) 
        {
            Groups = new List<Group>(practiceContext.GetInstance().Groups.Include( s => s.Speciality).ToList());

            GetStudents = new Command(() =>
            {
                if(SelectedGroup == null)
                {
                    MessageBox.Show("Вы не выбрали группу");
                    return;
                }
                else
                {
                    mainVM.SetPage(new ListStudentPage(SelectedGroup, mainVM));
                }
            });
        }
    }
}
