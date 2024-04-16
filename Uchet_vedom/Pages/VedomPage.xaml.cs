using Microsoft.Office.Interop.Excel;
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

using Excel = Microsoft.Office.Interop.Excel;


namespace Uchet_vedom.Pages
{
    /// <summary>
    /// Логика взаимодействия для VedomPage.xaml
    /// </summary>
    public partial class VedomPage : System.Windows.Controls.Page
    {
        public VedomPage()
        {
            InitializeComponent();
            List<vedom> ved = Core.DB.vedom.ToList();
            VedomDataGrid.ItemsSource = ved;

            uchenikComboBox.ItemsSource = Core.DB.uchenikk.ToList();
            predmetComboBox.ItemsSource = Core.DB.predmet.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            this.NavigationService.Navigate(new Pages.AddVedomPage());
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (VedomDataGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < VedomDataGrid.SelectedItems.Count; i++)
                {
                    uchenikk u = VedomDataGrid.SelectedItems[i] as uchenikk;
                    if (u != null)
                    {
                        Core.DB.uchenikk.Remove(u);
                        MessageBox.Show("Запись удалена");
                    }
                }
            }

            Core.DB.SaveChanges();
            List<vedom> ved = Core.DB.vedom.ToList();
            VedomDataGrid.ItemsSource = ved;
        }

        private void VceButton_Click(object sender, RoutedEventArgs e)
        {
            List<vedom> ved = Core.DB.vedom.ToList();
            VedomDataGrid.ItemsSource = ved;
        }

        private void F_ButtonApplication_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Сделать отчет в Excel?", "Отчет", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Excel.Application excel = new Excel.Application();
                excel.Visible = true;
                Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
                for (int j = 0; j < VedomDataGrid.Columns.Count; j++)//заголовки таблиц
                {
                    Range myRange = (Range)sheet1.Cells[1, j + 1];
                    sheet1.Cells[1, j + 1].Font.Bold = true;//Чтобы заголовок был жирным шрифтом
                    sheet1.Columns[j + 1].ColumnWidth = 15;//Настройка ширины столбца
                    myRange.Value2 = VedomDataGrid.Columns[j].Header;
                }
                for (int i = 0; i < VedomDataGrid.Columns.Count; i++)
                {
                    for (int j = 0; j < VedomDataGrid.Items.Count; j++)
                    {
                        TextBlock b = VedomDataGrid.Columns[i].GetCellContent(VedomDataGrid.Items[j]) as TextBlock;
                        if (b == null)
                            continue;

                        Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                        myRange.Value2 = b.Text;
                    }
                }
            }

        }

        private void izmButtonApplication_Click(object sender, RoutedEventArgs e)
        {
            if (VedomDataGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < VedomDataGrid.SelectedItems.Count; i++)
                {
                    vedom v = VedomDataGrid.SelectedItems[i] as vedom;
                    if (v != null)
                    {
                        MessageBox.Show("Запись изменена");
                    }
                }
            }

            Core.DB.SaveChanges();
            List<vedom> ved = Core.DB.vedom.ToList();
            VedomDataGrid.ItemsSource = ved;
        }

        private void TovarFilterButton_Click(object sender, RoutedEventArgs e)
        {
            var uch = uchenikComboBox.SelectedItem;
            if (uch == null)
            {
                MessageBox.Show("Данные не выбраны");
                return;
            }
            var selectedUchenik = uch as uchenikk;
            var UchenikFrom = Core.DB.vedom.Where(a => a.id_uchenik == selectedUchenik.id_uchenik).ToList();
            VedomDataGrid.ItemsSource = UchenikFrom;
        }

        private void PredmetFilterButton_Click(object sender, RoutedEventArgs e)
        {
            var pred = predmetComboBox.SelectedItem;
            if (pred == null)
            {
                MessageBox.Show("Данные не выбраны");
                return;
            }
            var selectedPredmet = pred as predmet;
            var PredmetFrom = Core.DB.vedom.Where(a => a.id_predmet == selectedPredmet.id_predmet).ToList();
            VedomDataGrid.ItemsSource = PredmetFrom;
        }

        private void predmetComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
