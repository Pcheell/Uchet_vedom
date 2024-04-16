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
    /// Логика взаимодействия для AddUchenikPage.xaml
    /// </summary>
    public partial class AddUchenikPage : Page
    {
        public AddUchenikPage()
        {
            InitializeComponent();
            id_classComboBox.ItemsSource = Core.DB.klass.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var id_u = id_uchenikTextBox.Text;
            var id_cl = id_classComboBox.SelectedValue;
            var fio = FIOTextBox.Text;
            var dr = d_rPicker.SelectedDate;
            var ad = adresTextBox.Text;
            var t = telTextBox.Text;
            
            
            var c = id_classComboBox.SelectedItem;
            if (c == null)
            {
                MessageBox.Show("Вы не ввели данные");
                return;
            }
            var selectedClass = c as klass;
            var classFromOnUch = Core.DB.klass.Where(a => a.id_class == selectedClass.id_class).ToList();

            var uch = new uchenikk();
            uch.id_uchenik = Convert.ToInt32(id_u);
            uch.id_class = Convert.ToInt32(id_cl);
            uch.FIO = fio;
            uch.d_r = Convert.ToDateTime(dr);
            uch.adres = ad;
            uch.tel = t;

            Core.DB.uchenikk.Add(uch);
            Core.DB.SaveChanges();
            MessageBox.Show("Информация добавлена");
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
