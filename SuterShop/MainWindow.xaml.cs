using SuterShop.AdminPanel.View;
using SuterShop.AdminPanel.ViewModel;
using SuterShop.Logining.View;
using SuterShop.Logining.ViewModel;
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

        private void OpenAdminPanel(object sender, RoutedEventArgs e)
        {
            var view = new AdminPanelView();
            view.ShowDialog();
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            (Application.Current as IApp).CurrentUser = null;
            logoutBtn.Visibility = Visibility.Hidden;
        }
    }
}
