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
    /// Логика взаимодействия для ClassPage.xaml
    /// </summary>
    public partial class ClassPage : System.Windows.Controls.Page
    {
        public ClassPage()
        {
            InitializeComponent();
            List<klass> clas = Core.DB.klass.ToList();
            ClassDataGrid.ItemsSource = clas;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Pages.AddClassPage());
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClassDataGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < ClassDataGrid.SelectedItems.Count; i++)
                {
                    klass cl = ClassDataGrid.SelectedItems[i] as klass;
                    if (cl != null)
                    {
                        Core.DB.klass.Remove(cl);
                        MessageBox.Show("Запись удалена");
                    }
                }
            }

            Core.DB.SaveChanges();
            List<klass> clas = Core.DB.klass.ToList();
            ClassDataGrid.ItemsSource = clas;
        }

        private void VceButton_Click(object sender, RoutedEventArgs e)
        {
            List<klass> clas = Core.DB.klass.ToList();
            ClassDataGrid.ItemsSource = clas; ;
        }

        private void F_ButtonApplication_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Сделать отчет в Excel?", "Отчет", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Excel.Application excel = new Excel.Application();
                excel.Visible = true;
                Workbook workbook = excel.Workbooks.Add(System.Reflection.Missing.Value);
                Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
                for (int j = 0; j < ClassDataGrid.Columns.Count; j++)//заголовки таблиц
                {
                    Range myRange = (Range)sheet1.Cells[1, j + 1];
                    sheet1.Cells[1, j + 1].Font.Bold = true;//Чтобы заголовок был жирным шрифтом
                    sheet1.Columns[j + 1].ColumnWidth = 15;//Настройка ширины столбца
                    myRange.Value2 = ClassDataGrid.Columns[j].Header;
                }
                for (int i = 0; i < ClassDataGrid.Columns.Count; i++)
                {
                    for (int j = 0; j < ClassDataGrid.Items.Count; j++)
                    {
                        TextBlock b = ClassDataGrid.Columns[i].GetCellContent(ClassDataGrid.Items[j]) as TextBlock;
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
            if (ClassDataGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < ClassDataGrid.SelectedItems.Count; i++)
                {
                    klass cl = ClassDataGrid.SelectedItems[i] as klass;
                    if (cl != null)
                    {
                        MessageBox.Show("Запись изменена");
                    }
                }
            }
            Core.DB.SaveChanges();
            List<klass> clas = Core.DB.klass.ToList();
            ClassDataGrid.ItemsSource = clas;
        }
    }
}
