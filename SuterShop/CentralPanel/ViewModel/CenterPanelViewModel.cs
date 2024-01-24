using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using SuterShop.card.view;
using SuterShop.card.viewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SuterShop.CentralPanel.View
{
    internal partial class CenterPanelViewModel : ObservableObject
    {
        private DataBaseContext _db;
        public WrapPanel CentralWrapPanel { get; set; }

        public CenterPanelViewModel()
        {
            (Application.Current as IApp).GoodItemCountChanged += GoodItemCountChanged;
        }

        private void GoodItemCountChanged()
        {
            Application.Current.Dispatcher.Invoke(() => { 
                SetData(); 
            });
        }

        internal void SetData()
        {
            _db = (Application.Current as IApp).Db;
            var goods = _db.GoodsForSaleList.Include("User").ToList();
            // Получаем папку куда пользователь установил нашу программу.
            var dir = $"{Directory.GetCurrentDirectory()}{System.IO.Path.DirectorySeparatorChar}TempImages{System.IO.Path.DirectorySeparatorChar}";

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            CentralWrapPanel.Children.Clear();
            foreach (var good in goods)
            {
                var fileName = $"{good.Category.Id}_{good.Id}.png";
                if (!File.Exists($"{dir}{fileName}"))
                {
                    File.WriteAllBytes($"{dir}{fileName}", good.Image);
                }




                var card = new cardView();

                card.Margin = new Thickness(5);
                (card.DataContext as cardViewModel).SetData(goods, good, $"{dir}{fileName}");
                CentralWrapPanel.Children.Add(card);
            }
        }

        internal void Filter(List<Category> categoriesFilter)
        {
            var list = new List<Category>(categoriesFilter);
            if (categoriesFilter.Count == 0)
            {
                foreach (var el in CentralWrapPanel.Children)
                {
                    int n = CentralWrapPanel.Children.IndexOf(el as cardView);
                    CentralWrapPanel.Children[n].Visibility = Visibility.Visible;
                }
                return;
            }

            foreach (var el in CentralWrapPanel.Children)
            {
                Category t = ((el as cardView).DataContext as cardViewModel).Good.Category;
                int n = CentralWrapPanel.Children.IndexOf(el as cardView);
                bool v = true;
                foreach (var c in categoriesFilter) if (c.Name == t.Name && c.Id == t.Id) v = false;
                if (v) CentralWrapPanel.Children[n].Visibility = Visibility.Collapsed;
                else CentralWrapPanel.Children[n].Visibility = Visibility.Visible;
            }
        }

        internal void SetData(WrapPanel myCentralWrapPanel)
        {
            CentralWrapPanel = myCentralWrapPanel;
        }
    }
}
