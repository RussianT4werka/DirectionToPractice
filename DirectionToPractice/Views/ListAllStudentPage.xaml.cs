using DirectionToPractice.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для ListAllStudentPage.xaml
    /// </summary>
    public partial class ListAllStudentPage : Page
    {
        public ListAllStudentPage(DB.Models.Practice practice, MainWindowVM mainVM, DB.Models.Speciality? selectedSpeciality)
        {
            InitializeComponent();
            DataContext = new ListAllStudentPageVM(practice, mainVM, selectedSpeciality);
        }
        private void Ai(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex()).ToString();
        }
    }
}
