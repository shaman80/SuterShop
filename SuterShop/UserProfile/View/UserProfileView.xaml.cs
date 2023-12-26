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
    
    public partial class UserProfileView : Window
    {

        private User _user;
        private DataBaseContext _db;

        public UserProfileView()
        {
            InitializeComponent ();
            if (_user != null)
            {
                UserPanel.Visibility= Visibility.Visible;

            }
            _db = (Application.Current as IApp).Db;
        }
        private void SetData(User user)
        {

            login.Text = _user.Login;
        }
    }
}
