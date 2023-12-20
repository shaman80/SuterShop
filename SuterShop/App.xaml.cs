using SuterShop.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
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
        public App()
        {
            var cs = "Server=192.168.88.54;Database=shop;Uid=root;Pwd=1q2w3e;";
            Db = new DataBaseContext(cs);
            //Db.Database.EnsureDeleted();
            Db.Database.EnsureCreated();
            CreateDefaultAdmin();

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
