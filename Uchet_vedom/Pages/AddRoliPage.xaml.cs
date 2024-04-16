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
    /// Логика взаимодействия для AddRoliPage.xaml
    /// </summary>
    public partial class AddRoliPage : Page
    {
        public AddRoliPage()
        {
            InitializeComponent();
            List<roli> rol = Core.DB.roli.ToList();
            RoliDataGrid.ItemsSource = rol;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var nroli = naim_roliTextBox.Text;
            var id_r = id_roliTextBox.Text;
            var log = loginTextBox.Text;
            var par = parolTextBox.Text;

            var r = new roli();

            r.id_roli = Convert.ToInt32(id_r);
            r.naim_roli = nroli;
            r.login = log;
            r.parol = par;

            if (nroli != null)
            {
                Core.DB.roli.Add(r);
                Core.DB.SaveChanges();
            }
            MessageBox.Show("Новая роль добавлена");
            List<roli> rol = Core.DB.roli.ToList();
            RoliDataGrid.ItemsSource = rol;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
