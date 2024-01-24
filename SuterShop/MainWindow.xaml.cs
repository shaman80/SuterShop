using SuterShop.AdminPanel.View;
using SuterShop.LoginingPanel.View;
using SuterShop.LoginingPanel.ViewModel;
using SuterShop.RegisterPanel.View;
using SuterShop.RegisterPanel.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

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
         
            List<string> styles = new List<string> { "light", "dark" };
            styleBox.SelectionChanged += ThemeChange;
            styleBox.ItemsSource = styles;
            styleBox.SelectedItem = "dark";
        }


        private void ThemeChange(object sender, SelectionChangedEventArgs e)
        {

            string style = styleBox.SelectedItem as string;
            // определяем путь к файлу ресурсов
            var uri = new Uri("Thems/"+ style + ".xaml", UriKind.Relative);
            // загружаем словарь ресурсов
            ResourceDictionary resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            // очищаем коллекцию ресурсов приложения
            Application.Current.Resources.Clear();
            // добавляем загруженный словарь ресурсов
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
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
