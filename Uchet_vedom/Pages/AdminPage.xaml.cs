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

namespace Uchet_vedom.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        private void Class_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Pages.ClassPage());
        }

        private void Roli_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Pages.RoliPage());
        }

        private void Predmet_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Pages.PredmetPage());
        }
    }
}
