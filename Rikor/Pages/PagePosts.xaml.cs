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
    /// Логика взаимодействия для PagePosts.xaml
    /// </summary>
    public partial class PagePosts : Page
    {
        public PagePosts()
        {
            InitializeComponent();
            PostsGrid.ItemsSource = RikorEntities.GetContext().Posts.ToList();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new PageAddEditPost(null));
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            var postsForRemoving = PostsGrid.SelectedItems.Cast<Posts>().ToList();
            if (MessageBox.Show($"Вы действительно хотите удалить сделующие {postsForRemoving.Count()} должностей?",
                    "Внимание",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    RikorEntities.GetContext().Posts.RemoveRange(postsForRemoving);
                    RikorEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    PostsGrid.ItemsSource = RikorEntities.GetContext().Posts.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            FrameApp.frmObj.Navigate(new PageAddEditPost((sender as Button).DataContext as Posts));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                RikorEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                PostsGrid.ItemsSource = RikorEntities.GetContext().Posts.ToList();
            }
        }
    }
}
