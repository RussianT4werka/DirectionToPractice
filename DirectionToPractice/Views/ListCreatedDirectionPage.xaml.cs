using DirectionToPractice.Classes;
using DirectionToPractice.DB;
using DirectionToPractice.DB.Models;
using DirectionToPractice.Tools;
using DirectionToPractice.Views.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DirectionToPractice.Views
{
    /// <summary>
    /// Логика взаимодействия для ListStudentPage.xaml
    /// </summary>
    public partial class ListCreatedDirectionPage : Page, INotifyPropertyChanged
    {
        private ObservableCollection<StudentPractice> studentPractices;
        private StudentPractice selectedStudentPractice1;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void SignalChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public ObservableCollection<StudentPractice> StudentPractices
        {
            get => studentPractices;
            set
            {
                studentPractices = value;
                SignalChanged();
            }
        }

        public StudentPractice SelectedStudentPractice 
        { 
            get => selectedStudentPractice1;
            set
            {
                selectedStudentPractice1 = value;
                SignalChanged();
            }
        }
        public List<Speciality> Specialities { get; set; }
        public List<string> PracticeTypes { get; set; }
        public Practice Practice { get; set; }
        public List<Teacher> Teachers { get; set; }
        public ListCreatedDirectionPage(MainWindowVM mainVM)
        {
            InitializeComponent();
            DataContext = this;
            StudentPractices = new ObservableCollection<StudentPractice>(practiceContext.GetInstance().StudentPractices.Include(s => s.Student).Include(s => s.Practice).ToList());
            Specialities = practiceContext.GetInstance().Specialities.ToList();
            PracticeTypes = new List<string> { "учебной", "производственной", "преддипломной" };
            Teachers = practiceContext.GetInstance().Teachers.ToList();
        }
        private void Ai(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void OpenDocument(object sender, RoutedEventArgs e)
        {
            if(SelectedStudentPractice.SelectedSpeciality != null && SelectedStudentPractice.Course >= 1 && SelectedStudentPractice.Course <= 4)
            {
                DirectionCreator.GetDirections(SelectedStudentPractice.Student, SelectedStudentPractice.Practice, SelectedStudentPractice.SelectedSpeciality, SelectedStudentPractice.Course);
            }
            else
            {
                MessageBox.Show("Не все поля заполнены!");
            }
        }
    }
}
