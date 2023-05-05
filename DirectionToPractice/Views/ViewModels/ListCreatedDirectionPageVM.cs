﻿using DirectionToPractice.DB;
using DirectionToPractice.DB.Models;
using DirectionToPractice.Tools;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DirectionToPractice.Views.ViewModels
{
    public class ListCreatedDirectionPageVM: BaseVM
    {
        private ObservableCollection<StudentPractice> studentPractices;
        private Student selectedStudent;
        public ObservableCollection<StudentPractice> StudentPractices
        {
            get => studentPractices;
            set
            {
                studentPractices = value;
                SignalChanged();
            }
        }

        public Student SelectedStudent
        {
            get => selectedStudent;
            set
            {
                selectedStudent = value;
                SignalChanged();
            }
        }
        public Command ToCreateDirectionPage { get; set; }
        public ListCreatedDirectionPageVM(MainWindowVM mainVM)
        {
            StudentPractices = new ObservableCollection<StudentPractice>(practiceContext.GetInstance().StudentPractices.Include( s => s.Student).Include(s => s.Practice).ToList());

            ToCreateDirectionPage = new Command(() =>
            {
                if (SelectedStudent == null)
                {
                    MessageBox.Show("Вы не выбрали студента");
                    return;
                }
                else
                {
                    //mainVM.SetPage(new CreateDirectionPage(SelectedStudent, mainVM));
                }
            });
        }
    }
}
