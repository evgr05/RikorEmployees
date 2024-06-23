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
    /// Логика взаимодействия для PageLogin.xaml
    /// </summary>
    public partial class PageLogin : Page
    {
        public PageLogin()
        {
            InitializeComponent();
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {

            var userObj = OdbConnectHelper.entObj.Users.FirstOrDefault(x => x.Login == txbLogin.Text && x.Password == psbPassword.Password);
            if (userObj == null)
            {
                MessageBox.Show("Такой пользователь не найден.",
                "Уведомление",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
            }
            else
            {
                switch (userObj.Role)
                {
                    case 1:
                        FrameApp.frmObj.Navigate(new PageEmployees());
                        break;

                }
            }
        }
    }
}
