﻿using SuterShop.AdminPanel.View;
using SuterShop.card.viewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SuterShop.card.view
{
    /// <summary>
    /// Логика взаимодействия для cardView.xaml
    /// </summary>
    public partial class cardView : UserControl
    {
        public cardView()
        {
            InitializeComponent();
            (Application.Current as IApp).UserIsLogining += UserIsLogining;
        }

        private void UserIsLogining(User user)
        {
            if (user.Status == Statuses.Seller)
            {
                EditGoodsItem.Visibility = Visibility.Visible;
            }
            else
            {
                EditGoodsItem.Visibility = Visibility.Collapsed;
            }
        }

        private void EditGoodsItemClick(object sender, RoutedEventArgs e)
        {
            var goodItem = ((sender as Button).DataContext as cardViewModel).Good;
            var view = new AdminPanelView();
            view.OpenEditGoodsItem(goodItem);
            view.ShowDialog();
        }

        private void AddToCard(object sender, RoutedEventArgs e)
        {

        }

        private void OpenDescription(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteGoodItem(object sender, RoutedEventArgs e)
        {
            //deleteFunc
        }

        private void RightButtonClick(object sender, MouseButtonEventArgs e)
        {
            var image = (sender as Image);
            var goodForSale = (image.DataContext as cardViewModel).Good;

            image.ContextMenu = new ContextMenu();
            var menuItem = new MenuItem
            {
                Header = "Добавить в корзину",
            };
            menuItem.Click += AddToCard;
            image.ContextMenu.Items.Add(menuItem);

            menuItem = new MenuItem
            {
                Header = "Открыть описание товара",
            };
            menuItem.Click += OpenDescription;
            image.ContextMenu.Items.Add(menuItem);

            image.ContextMenu.Items.Add(new Separator());

            if((Application.Current as IApp).CurrentUser?.Status == Statuses.Seller)
            {

                // TODO! доделать удаление карточки товара.
                
            }

        }

    }
}
