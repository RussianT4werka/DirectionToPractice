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
    /// Логика взаимодействия для ListStudentPage.xaml
    /// </summary>
    public partial class ListStudentPage : Page
    {
        public ListStudentPage(DB.Models.Group selectedGroup)
        {
            InitializeComponent();
            DataContext = new ListStudentPageVM(selectedGroup);
        }
        private void Ai(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex()).ToString();
        }
    }
}
