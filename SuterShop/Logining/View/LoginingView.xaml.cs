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

namespace SuterShop.Logining.View
{
    /// <summary>
    /// Логика взаимодействия для LoginingView.xaml
    /// </summary>
    public partial class LoginingView : Window
    {
        public LoginingView()
        {
            InitializeComponent();
        }
        private void Logining(object sender, RoutedEventArgs e)
        {
            var db = (Application.Current as IApp).Db;
            var users = db.Users.ToList();
            foreach (var user in users)
            {
                if (user.Login == userLogin.Text && user.Password == userPassword.Text)
                {
                    (Application.Current as IApp)!.CurrentUser = user;
                    return;
                }
            }
        }
    }
}
