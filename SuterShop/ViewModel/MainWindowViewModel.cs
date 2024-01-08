using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using SuterShop.CentralPanel.View;
using SuterShop.LeftPanel.View;
using SuterShop.LeftPanel.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SuterShop.ViewModel
{
    internal partial class MainWindowViewModel: ObservableObject
    {
        [ObservableProperty] private Control leftPanel;
        [ObservableProperty] private Control centerPanel;
        [ObservableProperty] private Control bottomPanel;
        [ObservableProperty] private User currentUser;

        private Timer _timer;
        private UserGuid userGuidJson;
        private DataBaseContext db = (Application.Current as IApp).Db;
        private Button _loginingBtn;
        private Button _logoutBtn;
        private Button _registerBtn;
        private Button _openAdminPanelBtn;
        private string _cs;
        


        public MainWindowViewModel()
        {
            //_cs = "Server=localhost;Database=shop2;Uid=root;Pwd=1q2w3e;";
            _cs = "Server=192.168.88.54;Database=shop2;Uid=root;Pwd=1q2w3e;";

            var leftPanelView = new LeftPanelView();
            (leftPanelView.DataContext as LeftPanelViewModel)!.SetData(); 
            leftPanel = leftPanelView;
            centerPanel = new CenterPanelView();
            (centerPanel.DataContext as CenterPanelViewModel)!.SetData();
            bottomPanel = new Label { Content = " Нижняя панель" };

            Thread.Sleep(1000);
            _timer = new Timer(TimerTick, null, 0, 60000);
            (Application.Current as IApp).СheckingLifeTime += СheckingLifeTime;
           
        }

  
        private void СheckingLifeTime()
        {
            Application.Current.Dispatcher.Invoke(() => {
                Logout();
            });
        }
        private void Logout()
        {
            (Application.Current as IApp).CurrentUser = null;
            _logoutBtn.Visibility = Visibility.Hidden;
            _openAdminPanelBtn.Visibility = Visibility.Hidden;
            _loginingBtn.Visibility = Visibility.Visible;
            _registerBtn.Visibility = Visibility.Visible;
        }
        private void TimerTick(object? state)
        {
            var db = new DataBaseContext(_cs);
            var _lifeTime =db.UserGuids.OrderByDescending(u => u.Id).FirstOrDefault();
            db.Database.CloseConnection();
            if (_lifeTime != null)
            {
                var time = DateTime.Now.Subtract(_lifeTime.LifeTime);
                if (time.Minutes > 5)
                {
                    (Application.Current as IApp).СheckingLifeTime?.Invoke();
                }
            }
            
        }
        internal void ShowPushProduct() //какая-то интересная функция, но над ней еще надо поработать
        {
          //  var window = new PushProduct();
          //  window.ShowDialog();
        }

        internal void filterCentral(List<Category> categoriesFilter)
        {
            (centerPanel.DataContext as CenterPanelViewModel).Filter(categoriesFilter);
        }

        internal void Logining(string fileName, Button loginingBtn, Button registerBtn, Button logoutBtn, Button openAdminPanelBtn)
        {
            if (File.Exists(fileName))
            {
                var jsonRead = File.ReadAllText(fileName);
                if (jsonRead != "")
                {
                   userGuidJson = JsonSerializer.Deserialize<UserGuid>(jsonRead);
                }
                var time = DateTime.Now.Subtract(userGuidJson.LifeTime);
                var guid = db.UserGuids.OrderByDescending(u => u.Guid).FirstOrDefault();

                if ((guid?.Guid == userGuidJson.Guid) && time.Minutes < 5)
                {
                    (Application.Current as IApp).CurrentUser = db.Users.Find(userGuidJson.UserId);
                    guid.LifeTime = DateTime.Now;
                    db.SaveChanges();
                }
            }

            _loginingBtn = loginingBtn;
            _registerBtn = registerBtn;
            _logoutBtn = logoutBtn;
            _openAdminPanelBtn = openAdminPanelBtn;

            if ((Application.Current as IApp).CurrentUser != null)
            {
                _loginingBtn.Visibility = Visibility.Hidden;
                _registerBtn.Visibility = Visibility.Hidden;
                _logoutBtn.Visibility = Visibility.Visible;
                _openAdminPanelBtn.Visibility = Visibility.Visible;
            }
            CurrentUser = (Application.Current as IApp).CurrentUser;
        }
    }
}
