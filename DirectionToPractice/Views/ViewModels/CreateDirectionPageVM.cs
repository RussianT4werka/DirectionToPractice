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
        private PracticeType selectedPracticeType;
        private List<ModulePractice> modulePractices;
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

        public List<PracticeType> PracticeTypes { get; set; }
        public PracticeType SelectedPracticeType
        {
            get => selectedPracticeType;
            set
            {
                selectedPracticeType = value;
                Filter();
                SignalChanged();
            }
        }

        public List<ModulePractice> ModulePractices
        {
            get => modulePractices;
            set
            {
                modulePractices = value;
                SignalChanged();
            }
        }
        public ModulePractice SelectedModulePractice { get; set; }

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
                Filter();
                SignalChanged();
            }
        }

        public Command CreateDirection { get; set; }
        public CreateDirectionPageVM(MainWindowVM mainVM)
        {
            Filter();
            Specialitys = new List<Speciality>(practiceContext.GetInstance().Specialities.ToList());
            PracticeTypes = new List<PracticeType>(practiceContext.GetInstance().PracticeTypes.ToList());
            Teachers = new List<Teacher>(practiceContext.GetInstance().Teachers.ToList());
            CreateDirection = new Command(() =>
            {
                if (SelectedPracticeType != null || SelectedSpeciality != null || SelectedTeacher != null || CountHours != 0 ) // Какие-то ебанутые условия
                {
                    if(DateStart.Date >= DateTime.Now || DateEnd.Date >= DateStart) //Тут вообще неясно почему их C# и в хуй не ставит...
                    {
                        Practice = new Practice() { Organisation = SpeciesOrganisation, City = City, StreetHouse = StreetHouse, PracticeTypeId = SelectedPracticeType.Id, ModulePracticeId = SelectedModulePractice?.Id, TeacherId = SelectedTeacher.Id, DateStart = DateStart, DateEnd = DateEnd, CountHours = CountHours };
                        mainVM.SetPage(new ListAllStudentPage(Practice, mainVM, SelectedSpeciality));
                    }
                    else
                    {
                        MessageBox.Show("Даты выставленны неверно");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Не все поля заполнены. Пустыми вы можете оставить лишь поля: \"Организация\", \"Населённый пункт\", \"Улица и дом\"");
                    return;
                }

            });
        }
        private void Filter()
        {
            IQueryable<ModulePractice> modulePractices = practiceContext.GetInstance().ModulePractices;
            if (SelectedPracticeType != null)
                modulePractices = modulePractices.Where(s => s.PracticeTypeId == SelectedPracticeType.Id);
            if (SelectedSpeciality != null)
                modulePractices = modulePractices.Where(s => s.SpecialityId == SelectedSpeciality.Id);
            ModulePractices = modulePractices.ToList();
        }
    }
}
