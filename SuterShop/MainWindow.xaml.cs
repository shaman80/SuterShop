using SuterShop.AdminPanel.View;
using SuterShop.LoginingPanel.View;
using SuterShop.LoginingPanel.ViewModel;
using SuterShop.RegisterPanel.View;
using SuterShop.RegisterPanel.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;

namespace SuterShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserGuid userGuidJson;
        private string fileName = "UserGuid.json";
        private DataBaseContext db = (Application.Current as IApp).Db;
        public MainWindow()
        {
            InitializeComponent();
            Logining();
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

            var user = db.UserGuids.OrderByDescending(u => u.Id).FirstOrDefault();
            if (user != null)
            {
                db.UserGuids.Remove(user);
                db.SaveChanges();
            }
        }

        private void Logining()
        {
            if (File.Exists(fileName))
            {
                var jsonRead = File.ReadAllText(fileName);
                if (jsonRead != "")
                {
                    userGuidJson = JsonSerializer.Deserialize<UserGuid>(jsonRead);
                }
            }
            var time = DateTime.Now.Subtract(userGuidJson.LifeTime);
            var guid = db.UserGuids.OrderByDescending(u => u.Guid).FirstOrDefault();

            if ((guid?.Guid == userGuidJson.Guid) && time.Minutes < 5)
            {
                (Application.Current as IApp).CurrentUser = userGuidJson.User;
                guid.LifeTime = DateTime.Now;
                db.SaveChanges();
            }

            if ((Application.Current as IApp).CurrentUser != null)
            {
                LoginingBtn.Visibility = Visibility.Hidden;
                RegisterBtn.Visibility = Visibility.Hidden;
                LogoutBtn.Visibility = Visibility.Visible;
                OpenAdminPanelBtn.Visibility = Visibility.Visible;
            }
        }
        private void Logining(object sender, RoutedEventArgs e)
        {
            var view = new LoginingPanelView { DataContext = new LoginingPanelViewModel() };
            view.ShowDialog();

            if((Application.Current as IApp).CurrentUser != null)
            {
                var userGuid = new UserGuid
                {
                    User = (Application.Current as IApp).CurrentUser,
                    Guid = Guid.NewGuid(),
                    LifeTime = DateTime.Now
                };
                var user = db.UserGuids.OrderByDescending(u => u.Id).FirstOrDefault();
                if (user != null)
                {
                    db.UserGuids.Remove(user);
                    //db.SaveChanges();
                }
                db.UserGuids.Add(userGuid);
                db.SaveChanges();
                var json = JsonSerializer.Serialize(userGuid);
                File.WriteAllText(fileName, json);
            }
           
           
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
