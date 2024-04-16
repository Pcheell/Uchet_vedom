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
    /// Логика взаимодействия для AddPredmetPage.xaml
    /// </summary>
    public partial class AddPredmetPage : Page
    {
        public AddPredmetPage()
        {
            InitializeComponent();
            List<predmet> pred = Core.DB.predmet.ToList();
            PredmetDataGrid.ItemsSource = pred;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var n_pred = nazvTextBox.Text;
            var id_p = id_predmetTextBox.Text;
            var uch = uchitelTextBox.Text;
            var kol = kol_chasovTextBox.Text;

            var p = new predmet();

            p.id_predmet = Convert.ToInt32(id_p);
            p.nazv = n_pred;
            p.uchitel = uch;
            p.kol_chasov = Convert.ToInt32(kol);

            if (n_pred != null)
            {
                Core.DB.predmet.Add(p);
                Core.DB.SaveChanges();
            }
            MessageBox.Show("Новая информация добавлена");
            List<predmet> pred = Core.DB.predmet.ToList();
            PredmetDataGrid.ItemsSource = pred;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
