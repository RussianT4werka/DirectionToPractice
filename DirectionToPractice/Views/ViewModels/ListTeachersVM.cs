using DirectionToPractice.DB;
using DirectionToPractice.DB.Models;
using DirectionToPractice.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DirectionToPractice.Views.ViewModels
{
    public class ListTeachersVM : BaseVM
    {
        private List<Teacher> teachers;

        public List<Teacher> Teachers 
        { 
            get => teachers;
            set
            {
                teachers = value;
                SignalChanged();
            }
        }
        public Teacher Teacher { get; set; }
        public Teacher SelectedTeacher { get; set; }
        public Command SaveTeacher { get; set; }
        public Command RemoveTeacher { get; set; }
        public string Surname { get; set; }
        public string NameF { get; set; }
        public string Patronymic { get; set; }
        public ListTeachersVM()
        {
            Teachers = practiceContext.GetInstance().Teachers.ToList();
            SaveTeacher = new Command(() =>
            {
                if (string.IsNullOrEmpty(Surname) || string.IsNullOrEmpty(NameF) || string.IsNullOrEmpty(Patronymic))
                {
                    MessageBox.Show("Не все поля заполнены", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    Teacher = new Teacher() { Surname = Surname, Name = NameF, Patronymic = Patronymic };
                    practiceContext.GetInstance().Teachers.Add(Teacher);
                    practiceContext.GetInstance().SaveChanges();
                    Teachers = new List<Teacher>(practiceContext.GetInstance().Teachers.ToList());
                    Surname = "";
                    NameF = "";
                    Patronymic = "";
                    SignalChanged(nameof(Surname));
                    SignalChanged(nameof(NameF));
                    SignalChanged(nameof(Patronymic));
                }
            });

            RemoveTeacher = new Command(() =>
            {
                if(SelectedTeacher == null)
                {
                    MessageBox.Show("Выберите преподавателя");
                    return;
                }
                else
                {
                    try
                    {
                        practiceContext.GetInstance().Remove(SelectedTeacher);
                        practiceContext.GetInstance().SaveChanges();
                        Teachers = new List<Teacher>(practiceContext.GetInstance().Teachers.ToList());
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка связи с БД");
                    }
                }
            });
        }
    }
}
