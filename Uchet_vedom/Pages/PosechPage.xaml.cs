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
    /// Логика взаимодействия для PosechPage.xaml
    /// </summary>
    public partial class PosechPage : System.Windows.Controls.Page
    {
        public PosechPage()
        {
            InitializeComponent();
            List<posech> pos= Core.DB.posech.ToList();
            PosechDataGrid.ItemsSource = pos;

            uchenikComboBox.ItemsSource = Core.DB.uchenikk.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Pages.AddPosechPage());
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (PosechDataGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < PosechDataGrid.SelectedItems.Count; i++)
                {
                    uchenikk u = PosechDataGrid.SelectedItems[i] as uchenikk;
                    if (u != null)
                    {
                        Core.DB.uchenikk.Remove(u);
                        MessageBox.Show("Запись удалена");
                    }
                }
            }

            Core.DB.SaveChanges();
            List<posech> pos = Core.DB.posech.ToList();
            PosechDataGrid.ItemsSource = pos;
        }

        private void VceButton_Click(object sender, RoutedEventArgs e)
        {
            List<posech> pos = Core.DB.posech.ToList();
            PosechDataGrid.ItemsSource = pos;
        }

        private void F_ButtonApplication_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Сделать отчет в Excel?", "Отчет", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Excel.Application excel = new Excel.Application();
                excel.Visible = true;
                Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
                for (int j = 0; j < PosechDataGrid.Columns.Count; j++)//заголовки таблиц
                {
                    Range myRange = (Range)sheet1.Cells[1, j + 1];
                    sheet1.Cells[1, j + 1].Font.Bold = true;//Чтобы заголовок был жирным шрифтом
                    sheet1.Columns[j + 1].ColumnWidth = 15;//Настройка ширины столбца
                    myRange.Value2 = PosechDataGrid.Columns[j].Header;
                }
                for (int i = 0; i < PosechDataGrid.Columns.Count; i++)
                {
                    for (int j = 0; j < PosechDataGrid.Items.Count; j++)
                    {
                        TextBlock b = PosechDataGrid.Columns[i].GetCellContent(PosechDataGrid.Items[j]) as TextBlock;
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
            if (PosechDataGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < PosechDataGrid.SelectedItems.Count; i++)
                {
                    posech p = PosechDataGrid.SelectedItems[i] as posech;
                    if (p != null)
                    {
                        MessageBox.Show("Запись изменена");
                    }
                }
            }

            Core.DB.SaveChanges();
            List<posech> pos = Core.DB.posech.ToList();
            PosechDataGrid.ItemsSource = pos;
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
            var UchenikFrom = Core.DB.posech.Where(a => a.id_uchenik == selectedUchenik.id_uchenik).ToList();
            PosechDataGrid.ItemsSource = UchenikFrom;
        }
    }
}
