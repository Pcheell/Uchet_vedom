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
    /// Логика взаимодействия для GLPage.xaml
    /// </summary>
    public partial class GLPage : Page
    {
        public GLPage()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            var login = LoginTextBox.Text;
            var pas = PasswordPasswordBox.Password;

            var user = Core.DB.roli.Where(u => u.login == login && u.parol == pas).FirstOrDefault();

            if (user != null)
            {
                if (login == "admin")
                {
                    this.NavigationService.Navigate(new Pages.AdminPage());
                }
                if (login == "uchitel")
                {
                    this.NavigationService.Navigate(new Pages.UchitelPage());
                }

            }
            else
            {
                MessageBox.Show("Такой пользователь не найден");
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти?", "Выход", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
            else return;
        }
    }
}
