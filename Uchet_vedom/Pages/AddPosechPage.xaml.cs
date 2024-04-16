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
    /// Логика взаимодействия для AddPosechPage.xaml
    /// </summary>
    public partial class AddPosechPage : Page
    {
        public AddPosechPage()
        {
            InitializeComponent();
            id_uchenikComboBox.ItemsSource = Core.DB.uchenikk.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var id_p = id_posechTextBox.Text;
            var id_u = id_uchenikComboBox.SelectedValue;
            var po_uv = po_uvajTextBox.Text;
            var po_neuv = po_neuvajTextBox.Text;
            var vs = vsegoTextBox.Text;
            var ch = chetvertTextBox.Text;
            var god = uch_godTextBox.Text;

            var p = new posech();
            p.id_posech = Convert.ToInt32(id_p);
            p.id_uchenik = Convert.ToInt32(id_u);
            p.po_uvaj = Convert.ToInt32(po_uv);
            p.po_neuvaj = Convert.ToInt32(po_neuv);
            p.vsego = Convert.ToInt32(vs);
            p.chetvert = ch;
            p.ucheb_god = Convert.ToInt32(god);

            var u = id_uchenikComboBox.SelectedItem;
            if (u == null)
            {
                MessageBox.Show("Вы не ввели данные");
                return;
            }
            var selectedUchenik = u as uchenikk;
            var uchenikFromOnVedom = Core.DB.uchenikk.Where(a => a.id_class == selectedUchenik.id_class).ToList();

            Core.DB.posech.Add(p);
            Core.DB.SaveChanges();
            MessageBox.Show("Информация добавлена");
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
