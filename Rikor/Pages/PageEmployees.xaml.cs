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
using Rikor.DataFiles;

namespace Rikor.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageEmployees.xaml
    /// </summary>
    public partial class PageEmployees : Page
    {
        public PageEmployees()
        {
            InitializeComponent();
            EmployeeGrid.ItemsSource = RikorEntities.GetContext().Employees.ToList();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new PageAddEdit((sender as Button).DataContext as Employees));
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new PageAddEdit(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                RikorEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                EmployeeGrid.ItemsSource = RikorEntities.GetContext().Employees.ToList();
            }
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            var employeesForRemoving = EmployeeGrid.SelectedItems.Cast<Employees>().ToList();
            if (MessageBox.Show($"Вы действительно хотите удалить сделующие {employeesForRemoving.Count()} сотрудников?",
                    "Внимание",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    RikorEntities.GetContext().Employees.RemoveRange(employeesForRemoving);
                    RikorEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    EmployeeGrid.ItemsSource = RikorEntities.GetContext().Employees.ToList();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void PostBtn_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new PagePosts());
            //
        }
    }
}
