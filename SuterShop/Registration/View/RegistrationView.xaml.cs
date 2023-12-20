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

namespace SuterShop.Registration
{
    /// <summary>
    /// Логика взаимодействия для RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : Window
    {
        public RegistrationView()
        {
            InitializeComponent();
        }

        private void AddNewByer(object sender, RoutedEventArgs e)
        {
            var byer = new User
            {
                FullName = byerName.Text,
                Login = byerLogin.Text,
                Email = byerEmail.Text,
                Password = byerPassword.Text,
                sum = 0,
                Status = Statuses.Buyer,
            };



            (DataContext as RegistrationViewModel).AddNewByer(byer);

            //byerName.Text = string.Empty;
            //byerLogin.Text = string.Empty;
            //byerEmail.Text = string.Empty;
            //byerPassword.Text = string.Empty;

            Close();
        }
    }
}
