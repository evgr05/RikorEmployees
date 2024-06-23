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
using Rikor.Pages;

namespace Rikor.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAddEdit.xaml
    /// </summary>
    public partial class PageAddEdit : Page
    {
        private Employees _currentEmployee = new Employees();
        public PageAddEdit(Employees selectedEmployee)
        {
            InitializeComponent();

            if (selectedEmployee != null)
            {
                _currentEmployee = selectedEmployee;
            }

            DataContext = _currentEmployee;
            ComboPosts.ItemsSource = RikorEntities.GetContext().Posts.ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentEmployee.LastName))
                errors.AppendLine("Укажите фамилию");
            if (string.IsNullOrWhiteSpace(_currentEmployee.FirstName))
                errors.AppendLine("Укажите имя");
            if (string.IsNullOrWhiteSpace(_currentEmployee.Patronymic))
                errors.AppendLine("Укажите отчество");
            if (string.IsNullOrWhiteSpace(_currentEmployee.Phone))
                errors.AppendLine("Укажите телефон");
            if (_currentEmployee.Posts == null)
            {
                errors.AppendLine("Укажите должность");
            }
            if (_currentEmployee.Employment == null)
                errors.AppendLine("Укажите дату приема на работу");
            if (_currentEmployee.Pay == null)
                errors.AppendLine("Укажите оклад");
            if (string.IsNullOrWhiteSpace(_currentEmployee.email))
                errors.AppendLine("Укажите почту");
            if (string.IsNullOrWhiteSpace(_currentEmployee.Adress))
                errors.AppendLine("Укажите адрес");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentEmployee.Id == 0)
                RikorEntities.GetContext().Employees.Add(_currentEmployee);
            try
            {
                RikorEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                FrameApp.frmObj.GoBack();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }

        private void ComboPosts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
