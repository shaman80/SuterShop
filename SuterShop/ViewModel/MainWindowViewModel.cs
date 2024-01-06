using CommunityToolkit.Mvvm.ComponentModel;
using SuterShop.CentralPanel.View;
using SuterShop.LeftPanel.View;
using SuterShop.LeftPanel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SuterShop.ViewModel
{
    internal partial class MainWindowViewModel: ObservableObject
    {
        [ObservableProperty] private Control leftPanel;
        [ObservableProperty] private Control centerPanel;
        [ObservableProperty] private Control bottomPanel;
        [ObservableProperty] private User currentUser;
     
        public MainWindowViewModel()
        {

            var leftPanelView = new LeftPanelView();
            (leftPanelView.DataContext as LeftPanelViewModel)!.SetData(); 
            leftPanel = leftPanelView;
            centerPanel = new CenterPanelView();
            (centerPanel.DataContext as CenterPanelViewModel)!.SetData();
            bottomPanel = new Label { Content = " Нижняя панель" };
        }

        internal void ShowPushProduct() //какая-то интересная функция, но над ней еще надо поработать
        {
          //  var window = new PushProduct();
          //  window.ShowDialog();
        }

        internal void filterCentral(List<Category> categoriesFilter)
        {
            (centerPanel.DataContext as CenterPanelViewModel).Filter(categoriesFilter);
        }

    }
}
