using SuterShop.AdminPanel.View;
using SuterShop.AdminPanel.ViewModel;
using SuterShop.ViewModel;
using System.Windows;

namespace SuterShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as MainWindowViewModel).ShowPushProduct();
        }

        private void OpenAdminPanel(object sender, RoutedEventArgs e)
        {
            var view = new AdminPanelView();
            view.ShowDialog();
            if((Application.Current as IApp).CurrentUser != null)
            {
                logoutBtn.Visibility = Visibility.Visible;
            }
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            (Application.Current as IApp).CurrentUser = null;
            logoutBtn.Visibility = Visibility.Hidden;
        }
    }
}
