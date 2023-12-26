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

namespace SuterShop.UserProfile.View
{
    /// <summary>
    /// Логика взаимодействия для UserProfileView.xaml
    /// </summary>
    public partial class UserProfileView : Window
    {
        private User _user;
        private DataBaseContext _db;

        public UserProfileView()
        {
            InitializeComponent();
            _user = (Application.Current as IApp).CurrentUser;
            if (_user != null)
            {
                userPanel.Visibility = Visibility.Visible;
            }
            _db = (Application.Current as IApp).Db;
        }

        internal void SetData(User user)
        {
            _user = user;
             login.Text = _user.Login;
             fullName.Text = _user.FullName;
             email.Text = _user.Email;
        }
    }
}
