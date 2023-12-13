using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SuterShop.LeftPanel.ViewModel
{
    internal partial class LeftPanelViewModel : ObservableObject
    {
        [ObservableProperty] private ObservableCollection<Category> categoryes;
        private List<Category> categoriesFilter = new List<Category>();

        private DataBaseContext _db;
        internal void SetData()
        {

            _db = (Application.Current as IApp).Db;
            Categoryes = new ObservableCollection<Category>();
            var list = _db.Category.ToList();
            foreach (var category in list)
            {
                Categoryes.Add(new Category
                {
                    Id = category.Id,
                    Name = category.Name,
                });


            }
        }

        internal void Click(object sender, MouseButtonEventArgs e)
        {
            var item = categoryes.First((el => el.Name == (sender as TextBlock).Text));
            if (categoriesFilter.Exists((el => el.Name == (sender as TextBlock).Text)))
            {
                categoriesFilter.Remove((item));
                (Application.Current as IApp).filterCentral(categoriesFilter);
                return;
            }

            categoriesFilter.Add(item);
            (Application.Current as IApp).filterCentral(categoriesFilter);
        }
    }
}
