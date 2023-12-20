using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuterShop.Registration
{
    internal partial class RegistrationViewModel : ObservableObject
    {
        private DataBaseContext _db;

        [ObservableProperty] private User user;

        public RegistrationViewModel()
        {
            _db = (Application.Current as IApp).Db;
        }

        internal void AddNewByer(User byer)
        {
            var users = _db.Users.ToList();

            foreach (var user in users)
            {
                if (user.Login == byer.Login)
                {
                    MessageBox.Show("Логин занят");
                    return;
                }
            }
            _db.Users.Add(byer);
            _db.SaveChanges();
        }
    }


}
