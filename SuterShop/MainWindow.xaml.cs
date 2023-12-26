using SuterShop.AdminPanel.View;
using SuterShop.LoginingPanel.View;
using SuterShop.LoginingPanel.ViewModel;
using SuterShop.RegisterPanel.View;
using SuterShop.RegisterPanel.ViewModel;
using SuterShop.UserProfile.View;
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
            var user = (Application.Current as IApp).CurrentUser;
            if (user.Status == Statuses.Admin)
            {
                var view = new AdminPanelView();
                view.ShowDialog();
            }
            else if(user.Status == Statuses.Buyer)
            {
                var view = new UserProfileView();
                view.ShowDialog();
                view.SetData(user);
            }
            
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            (Application.Current as IApp).CurrentUser = null;
            LogoutBtn.Visibility = Visibility.Hidden;
            OpenAdminPanelBtn.Visibility = Visibility.Hidden;
            LoginingBtn.Visibility = Visibility.Visible;
            RegisterBtn.Visibility = Visibility.Visible;
        }

        private void Logining(object sender, RoutedEventArgs e)
        {
            var view = new LoginingPanelView { DataContext = new LoginingPanelViewModel() };
            view.ShowDialog();
            if((Application.Current as IApp).CurrentUser != null)
            {
                LoginingBtn.Visibility = Visibility.Hidden;
                RegisterBtn.Visibility = Visibility.Hidden;
                LogoutBtn.Visibility = Visibility.Visible;
                OpenAdminPanelBtn.Visibility = Visibility.Visible;
            }
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            var view = new RegisterPanelView { DataContext = new RegisterPanelViewModel() };
            view.ShowDialog();
            if ((Application.Current as IApp).CurrentUser != null)
            {
                LoginingBtn.Visibility = Visibility.Hidden;
                RegisterBtn.Visibility = Visibility.Hidden;
                LogoutBtn.Visibility = Visibility.Visible;
                OpenAdminPanelBtn.Visibility = Visibility.Visible;
            }
        }
    }
}
