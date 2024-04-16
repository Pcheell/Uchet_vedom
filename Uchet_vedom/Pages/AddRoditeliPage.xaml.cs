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
    /// Логика взаимодействия для AddRoditeliPage.xaml
    /// </summary>
    public partial class AddRoditeliPage : Page
    {
        public AddRoditeliPage()
        {
            InitializeComponent();
            id_uchenikComboBox.ItemsSource = Core.DB.uchenikk.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var id_r = id_roditelTextBox.Text;
            var id_uch = id_uchenikComboBox.SelectedValue;
            var s_r = step_rodTextBox.Text;
            var fio_r = fio_rodTextBox.Text;
            var ad = adresTextBox.Text;
            var t = telTextBox.Text;
            var m_r = mest_rabTextBox.Text;

            var r = new roditeli();
            r.id_roditel = Convert.ToInt32(id_r);
            r.id_uchenik = Convert.ToInt32(id_uch);
            r.step_rod = s_r;
            r.FIO = fio_r;
            r.adres = ad;
            r.tel = t;
            r.mest_rab = m_r;

            var u = id_uchenikComboBox.SelectedItem;
            if (u == null)
            {
                MessageBox.Show("Вы не ввели данные");
                return;
            }
            var selectedUchenik = u as uchenikk;
            var uchenikFromOnRod = Core.DB.uchenikk.Where(a => a.id_uchenik == selectedUchenik.id_uchenik).ToList();

            Core.DB.roditeli.Add(r);
            Core.DB.SaveChanges();
            MessageBox.Show("Информация добавлена");
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
