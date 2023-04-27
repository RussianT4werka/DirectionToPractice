﻿using DirectionToPractice.Classes;
using DirectionToPractice.DB;
using DirectionToPractice.DB.Models;
using DirectionToPractice.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Groups = new List<Group>(practiceContext.GetInstance().Groups.ToList());

            GetStudents = new Command(() =>
            {
                mainVM.CurrentPage = new ListStudentPage(SelectedGroup);
            });
        }
    }
}