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
    /// Логика взаимодействия для UchitelPage.xaml
    /// </summary>
    public partial class UchitelPage : Page
    {
        public UchitelPage()
        {
            InitializeComponent();
        }

        private void Uchenik_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Pages.UchenikPage());
        }

        private void Roditeli_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Pages.RoditeliPage());
        }

        private void Vedom_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Pages.VedomPage());
        }

        private void Posech_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Pages.PosechPage());
        }
    }
}
