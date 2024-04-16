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
    /// Логика взаимодействия для RoliPage.xaml
    /// </summary>
    public partial class RoliPage : System.Windows.Controls.Page
    {
        public RoliPage()
        {
            InitializeComponent();
            List<roli> rol = Core.DB.roli.ToList();
            RoliDataGrid.ItemsSource = rol;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Pages.AddRoliPage());
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (RoliDataGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < RoliDataGrid.SelectedItems.Count; i++)
                {
                    roli r = RoliDataGrid.SelectedItems[i] as roli;
                    if (r != null)
                    {
                        Core.DB.roli.Remove(r);
                        MessageBox.Show("Запись удалена");
                    }
                }
            }

            Core.DB.SaveChanges();
            List<roli> rol = Core.DB.roli.ToList();
            RoliDataGrid.ItemsSource = rol;
        }

        private void VceButton_Click(object sender, RoutedEventArgs e)
        {
            List<roli> rol = Core.DB.roli.ToList();
            RoliDataGrid.ItemsSource = rol;
        }

        private void F_ButtonApplication_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Сделать отчет в Excel?", "Отчет", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Excel.Application excel = new Excel.Application();
                excel.Visible = true;
                Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
                for (int j = 0; j < RoliDataGrid.Columns.Count; j++)//заголовки таблиц
                {
                    Range myRange = (Range)sheet1.Cells[1, j + 1];
                    sheet1.Cells[1, j + 1].Font.Bold = true;//Чтобы заголовок был жирным шрифтом
                    sheet1.Columns[j + 1].ColumnWidth = 15;//Настройка ширины столбца
                    myRange.Value2 = RoliDataGrid.Columns[j].Header;
                }
                for (int i = 0; i < RoliDataGrid.Columns.Count; i++)
                {
                    for (int j = 0; j < RoliDataGrid.Items.Count; j++)
                    {
                        TextBlock b = RoliDataGrid.Columns[i].GetCellContent(RoliDataGrid.Items[j]) as TextBlock;
                        if (b == null)
                            continue;

                        Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[j + 2, i + 1];
                        myRange.Value2 = b.Text;
                    }
                }
            }
            else return;
        }

        private void izmButtonApplication_Click(object sender, RoutedEventArgs e)
        {
            if (RoliDataGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < RoliDataGrid.SelectedItems.Count; i++)
                {
                    roli r = RoliDataGrid.SelectedItems[i] as roli;
                    if (r != null)
                    {
                        MessageBox.Show("Запись изменена");
                    }
                }
            }

            Core.DB.SaveChanges();
            List<roli> rol = Core.DB.roli.ToList();
            RoliDataGrid.ItemsSource = rol;
        }
    }
}
