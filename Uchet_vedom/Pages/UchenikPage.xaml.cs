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
    /// Логика взаимодействия для UchenikPage.xaml
    /// </summary>
    public partial class UchenikPage : System.Windows.Controls.Page
    {
        public UchenikPage()
        {
            InitializeComponent();
            List<uchenikk> uch = Core.DB.uchenikk.ToList();
            UchenikDataGrid.ItemsSource = uch;

            classComboBox.ItemsSource = Core.DB.klass.ToList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Pages.AddUchenikPage());
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (UchenikDataGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < UchenikDataGrid.SelectedItems.Count; i++)
                {
                    uchenikk u = UchenikDataGrid.SelectedItems[i] as uchenikk;
                    if (u != null)
                    {
                        Core.DB.uchenikk.Remove(u);
                        MessageBox.Show("Запись удалена");
                    }
                }
            }

            Core.DB.SaveChanges();
            List<uchenikk> uch = Core.DB.uchenikk.ToList();
            UchenikDataGrid.ItemsSource = uch;

        }

        private void VceButton_Click(object sender, RoutedEventArgs e)
        {
            List<uchenikk> uch = Core.DB.uchenikk.ToList();
            UchenikDataGrid.ItemsSource = uch;
        }

        private void F_ButtonApplication_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Сделать отчет в Excel?", "Отчет", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Excel.Application excel = new Excel.Application();
                excel.Visible = true;
                Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
                for (int j = 0; j < UchenikDataGrid.Columns.Count; j++)//заголовки таблиц
                {
                    Range myRange = (Range)sheet1.Cells[1, j + 1];
                    sheet1.Cells[1, j + 1].Font.Bold = true;//Чтобы заголовок был жирным шрифтом
                    sheet1.Columns[j + 1].ColumnWidth = 15;//Настройка ширины столбца
                    myRange.Value2 = UchenikDataGrid.Columns[j].Header;
                }
                for (int i = 0; i < UchenikDataGrid.Columns.Count; i++)
                {
                    for (int j = 0; j < UchenikDataGrid.Items.Count; j++)
                    {
                        TextBlock b = UchenikDataGrid.Columns[i].GetCellContent(UchenikDataGrid.Items[j]) as TextBlock;
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
            if (UchenikDataGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < UchenikDataGrid.SelectedItems.Count; i++)
                {
                    uchenikk u = UchenikDataGrid.SelectedItems[i] as uchenikk;
                    if (u != null)
                    {
                        MessageBox.Show("Запись изменена");
                    }
                }
            }

            Core.DB.SaveChanges();
            List<uchenikk> uch = Core.DB.uchenikk.ToList();
            UchenikDataGrid.ItemsSource = uch;
        }

        private void ClassFilterButton_Click(object sender, RoutedEventArgs e)
        {
            var cl = classComboBox.SelectedItem;
            if (cl == null)
            {
                MessageBox.Show("Данные не выбраны");
                return;
            }
            var selectedClass = cl as klass;
            var ClassFrom = Core.DB.uchenikk.Where(a => a.id_class == selectedClass.id_class).ToList();
            UchenikDataGrid.ItemsSource = ClassFrom;
        }

        private void name_uchenikTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var currentName = name_uchenikTextBox.Text;
            if (currentName == "")
            {
                MessageBox.Show("Пусто:");
                return;
            }
            var uchenikWihtThisName = Core.DB.uchenikk.Where(t => t.FIO.StartsWith(currentName)).ToList();
            UchenikDataGrid.ItemsSource = uchenikWihtThisName;
        }
    }
}
