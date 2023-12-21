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
using System.Windows.Shapes;

namespace SuterShop.RegisterPanel.View
{
    /// <summary>
    /// Логика взаимодействия для LoginingPanelView.xaml
    /// </summary>
    public partial class RegisterPanelView : Window
    {
        public RegisterPanelView()
        {
            InitializeComponent();
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            var db = (Application.Current as IApp).Db;
            var newUser = new User
            {
                Login = userLogin.Text,
                FullName = FullName.Text,
                Password = userPassword.Text,
                Email = userEmail.Text,
                Status = Statuses.Buyer,
            };
            (Application.Current as IApp).CurrentUser = newUser;
            db.Users.Add(newUser);
            db.SaveChanges();
            Close();
        }
    }
}
