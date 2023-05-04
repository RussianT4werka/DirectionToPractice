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
        public Command ListAllStudents { get; set; }
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
            SetPage(new ListCreatedDirectionPage(this));

            ListAllCreatedDirection = new Command(() =>
            {
                SetPage(new ListCreatedDirectionPage(this));
            });

            ListAllStudents = new Command(() =>
            {
                SetPage(new ListAllStudentPage(this));
            });
            Info = new Command(() =>
            {
                MessageBox.Show("Приложение создано с целью облегчить бумажную волокиту, а именно, ускорить процесс заполнения направления на практику " +
                "По кнопке \"Список групп\" вы перейдёте на их список, где можете выбрать нужную вам группу(правая кнопка мыши). После выбора группы " +
                "открывается список студентов состоящих в этой группе. Чтобы создать направление на нужного студента, его нужно выбрать из списка студентов. " +
                "На странице создания направления необходимо указать все поля. После заполнения нужно нажать на кнопку \"Выдать направление\". После этого " +
                "откроется готовый документ Word, который рекомендуется проверить на ошибки(программа не идеальна, поэтому в некоторых местах нужно будет добавить " +
                "некоторое кол-во нижник прочерков. Также некоторые имена неправильно склоняются, но таких немного (Дмитрий)");
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
