using SuterShop.Registration;
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

namespace SuterShop.LoginingPanel.View
{
    /// <summary>
    /// Логика взаимодействия для LoginingPanelView.xaml
    /// </summary>
    public partial class LoginingPanelView : Window
    {
        public LoginingPanelView()
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
                    Close();
                    return;
                }
            }
        }

        private void Registration(object sender, RoutedEventArgs e)
        {

            var registration = new RegistrationView { DataContext = new RegistrationViewModel() };
            registration.ShowDialog();

            Close();
        }
    }
}
