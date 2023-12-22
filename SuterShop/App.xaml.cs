using Microsoft.EntityFrameworkCore;
using SuterShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SuterShop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IApp
    {
        private User currentUser;

        private Timer _timer;

        public Timer TimerOnline { get; }

        private int _countGoods;
        private int _countMessages;

        public User CurrentUser
        {
            get => currentUser;
            set
            {
                UserIsLogining?.Invoke(value);
                currentUser = value;
            }
        }


        public DataBaseContext Db { get; set; }

        public delegate void UpdateUserDelegate(User user);
        public UpdateUserDelegate UserIsLogining { get; set; }

        public delegate void UpdateCardDelegate(GoodsForSale goodsForSale);
        public UpdateCardDelegate GoodItemChanged { get; set; }

        public delegate void UpdateShopDelegate();
        public UpdateShopDelegate GoodItemCountChanged { get; set; }
        public UpdateShopDelegate MessageItemCountChanged { get; set; }
        private string cs;
        public App()
        {
            cs = "Server=192.168.88.54;Database=sfy;Uid=root;Pwd=1q2w3e;";
            Db = new DataBaseContext(cs);
           // Db.Database.EnsureDeleted();
            Db.Database.EnsureCreated();
            
            CreateDefaultAdmin();
            Thread.Sleep(1000);
            _timer = new Timer(TimerTick, null,0, 3000);
            TimerOnline = new Timer(trackingOnlineStatuses,null, 0, 20000);
        }

        private void TimerTick(object? state)
        {
            var _db = new DataBaseContext(cs);
            var countGoods = _db.GoodsForSaleList.Count();
            var countMessages = _db.Messages.Count();
            if (_countGoods != countGoods)
            {
                GoodItemCountChanged?.Invoke();
            };
            _countGoods = countGoods; 
            if (_countMessages != countMessages)
            {
                MessageItemCountChanged?.Invoke();
            };
            _countMessages = countMessages;
        }
        private void trackingOnlineStatuses(object? state)
        {

            var _db = new DataBaseContext(cs);
            foreach (var elem in _db.OnlineUsers)
            {
                if (DateTime.Now.Minute - elem.timer.Minute > 5)
                {
                    _db.OnlineUsers.Remove(elem);
                }
            }
            _db.SaveChanges();
            if (CurrentUser != null)
            {
                User us = new User();
                var t = 0;
                foreach (var elem in _db.Users)
                {
                    if (elem.Id == CurrentUser.Id)
                    {
                        us = elem;
                    }
                }
                if (_db.OnlineUsers != null)
                {
                    foreach (var elem in _db.OnlineUsers)
                    {
                        if (elem.user.Id == CurrentUser.Id)
                        {
                            elem.timer = DateTime.Now;
                            _db.OnlineUsers.Update(elem);
                            t = 1;
                        }
                    }
                    if (t == 0)
                    {

                        var temp = new OnlineUser
                        {
                            user = us,
                            timer = DateTime.Now
                        };

                        _db.OnlineUsers.Add(temp);

                    }
                    _db.SaveChanges();
                }
            }
           
        }

        private void CreateDefaultAdmin()
        {
            if (!Db.Users.Any())
            {
                Db.Users.Add(new User
                {
                    Email = "default@supershop.ru",
                    FullName = "Администратор по умолчанию",
                    Login = "Admin",
                    Password = "123",
                    Status = Statuses.Admin
                });
                Db.SaveChanges();
            }
        }

        public void filterCentral(List<Category> categoriesFilter)
        {
            (MainWindow.DataContext as MainWindowViewModel).filterCentral(categoriesFilter);
        }
    }
}
