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
    /// Логика взаимодействия для AddVedomPage.xaml
    /// </summary>
    public partial class AddVedomPage : Page
    {
        public AddVedomPage()
        {
            InitializeComponent();

            id_predmetComboBox.ItemsSource = Core.DB.predmet.ToList();
            id_uchenikComboBox.ItemsSource = Core.DB.uchenikk.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var id_v = id_vedomTextBox.Text;
            var id_p = id_predmetComboBox.SelectedValue;
            var id_u = id_uchenikComboBox.SelectedValue;
            var oc = ocenkaTextBox.Text;
            var ch = chetvertTextBox.Text;
            var god = uch_godTextBox.Text;

            var v = new vedom();
            v.id_vedom = Convert.ToInt32(id_v);
            v.id_predmet = Convert.ToInt32(id_p);
            v.id_uchenik = Convert.ToInt32(id_u);
            v.ocenka = Convert.ToInt32(oc);
            v.chetvert = oc;
            v.ucheb_god = Convert.ToInt32(god);


            var p = id_predmetComboBox.SelectedItem;
            if (p == null)
            {
                MessageBox.Show("Вы не ввели данные");
                return;
            }
            var selectedPredmet = p as predmet;
            var predmetFromOnVedom = Core.DB.predmet.Where(a => a.id_predmet == selectedPredmet.id_predmet).ToList();

            var u = id_uchenikComboBox.SelectedItem;
            if (u == null)
            {
                MessageBox.Show("Вы не ввели данные");
                return;
            }
            var selectedUchenik = u as uchenikk;
            var uchenikFromOnVedom = Core.DB.uchenikk.Where(a => a.id_class == selectedUchenik.id_class).ToList();

            Core.DB.vedom.Add(v);
            Core.DB.SaveChanges();
            MessageBox.Show("Информация добавлена");
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
