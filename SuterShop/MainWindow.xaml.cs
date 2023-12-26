using SuterShop.AdminPanel.View;
using SuterShop.AdminPanel.ViewModel;
using SuterShop.LoginingPanel.View;
using SuterShop.LoginingPanel.ViewModel;
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
            LogoutBtn.Visibility = Visibility.Hidden;
            OpenAdminPanelBtn.Visibility = Visibility.Hidden;
            LoginingBtn.Visibility = Visibility.Visible;
        }

        private void Logining(object sender, RoutedEventArgs e)
        {
            var view = new LoginingPanelView { DataContext = new LoginingPanelViewModel() };
            view.ShowDialog();
            if((Application.Current as IApp).CurrentUser != null)
            {
                LoginingBtn.Visibility = Visibility.Hidden;
                LogoutBtn.Visibility = Visibility.Visible;
                OpenAdminPanelBtn.Visibility = Visibility.Visible;
            }


        }


        
    }
}
