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
    /// Логика взаимодействия для AddClassPage.xaml
    /// </summary>
    public partial class AddClassPage : Page
    {
        public AddClassPage()
        {
            InitializeComponent();
            List<klass> clas = Core.DB.klass.ToList();
            ClassDataGrid.ItemsSource = clas;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var id_c = id_classTextBox.Text;
            var n_cl = n_classTextBox.Text;
            var kl_ruk = klassn_rukTextBox.Text;
            
            var c = new klass();

            c.id_class = Convert.ToInt32(id_c);
            c.n_class = n_cl;
            c.klassn_ruk = kl_ruk;

            if (n_cl != null)
            {
                Core.DB.klass.Add(c);
                Core.DB.SaveChanges();
                MessageBox.Show("Новая информация добавлена");

            }
            else
            {
                MessageBox.Show("Вы не ввели данные");
                return;
            }

            Core.DB.SaveChanges();
            List<klass> clas = Core.DB.klass.ToList();
            ClassDataGrid.ItemsSource = clas;

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
