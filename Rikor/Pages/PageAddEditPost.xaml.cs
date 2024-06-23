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
    /// Логика взаимодействия для PageAddEditPost.xaml
    /// </summary>
    public partial class PageAddEditPost : Page
    {
        private Posts _currentPost = new Posts();
        public PageAddEditPost(Posts selectedPost)
        {
            InitializeComponent();
            if (selectedPost != null)
            {
                _currentPost = selectedPost;
            }

            DataContext = _currentPost;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {            
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentPost.Name))
                errors.AppendLine("Укажите должность");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentPost.Id == 0)
                RikorEntities.GetContext().Posts.Add(_currentPost);
            try
            {
                RikorEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                FrameApp.frmObj.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
