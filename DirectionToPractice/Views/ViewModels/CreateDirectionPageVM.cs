using DirectionToPractice.DB;
using DirectionToPractice.DB.Models;
using DirectionToPractice.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace DirectionToPractice.Views.ViewModels
{
    public class CreateDirectionPageVM : BaseVM
    {
        private bool block = true;
        private string selectedPracticeType;
        private Practice practice;
        private Speciality selectedSpeciality;

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
        
        public string SelectedPracticeType 
        { 
            get => selectedPracticeType;
            set
            {
                selectedPracticeType = value;
                SignalChanged();
            }
        }
        public string ModulePractice { get; set; }

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

        public List<Speciality> Specialitys { get; set; }

        public Speciality SelectedSpeciality
        {
            get => selectedSpeciality;
            set
            {
                selectedSpeciality = value;
                SignalChanged();
            }
        }

        public Command CreateDirection { get; set; }
        public CreateDirectionPageVM(MainWindowVM mainVM)
        {
            Specialitys = new List<Speciality>(practiceContext.GetInstance().Specialities.ToList());
            Teachers = new List<Teacher>(practiceContext.GetInstance().Teachers.ToList());
            CreateDirection = new Command(() =>
            {
                if (string.IsNullOrEmpty(SelectedPracticeType) || SelectedSpeciality == null || string.IsNullOrEmpty(ModulePractice) || SelectedTeacher == null || CountHours <= 1 || string.IsNullOrEmpty(SpeciesOrganisation) || string.IsNullOrEmpty(City) || string.IsNullOrEmpty(StreetHouse)) 
                {
                    MessageBox.Show("Не все поля заполнены или заполнены неверно!", "Смотрите внимательно", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    if (DateStart.Date != DateEnd.Date)
                    {
                        Practice = new Practice() { Organisation = SpeciesOrganisation, City = City, StreetHouse = StreetHouse, PracticeType = SelectedPracticeType, ModulePractice = ModulePractice, TeacherId = SelectedTeacher.Id, DateStart = DateStart, DateEnd = DateEnd, CountHours = CountHours };
                        mainVM.SetPage(new ListAllStudentPage(Practice, mainVM, SelectedSpeciality));
                    }
                    else
                    {
                        MessageBox.Show("Практика не может длиться 0 дней", "Проставьте даты", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                }

            });
        }
    }
}
