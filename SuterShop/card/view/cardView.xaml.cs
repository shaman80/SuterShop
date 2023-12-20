using SuterShop.AdminPanel.View;
using SuterShop.card.viewModel;
using System;
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
            if (user?.Status == Statuses.Seller)
            {
                EditGoodsItem.Visibility = Visibility.Visible;
            }
            else if (user?.Status == Statuses.Buyer)
            {
                ReviewBtn.Visibility = Visibility.Visible;
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


        private void DeleteGoodItem(GoodsForSale goodForSale)
        {
            (DataContext as cardViewModel)!.DeleteGoodItem(goodForSale);
            (VisualParent as WrapPanel).Children.Remove(this);
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

            var currentUser = (Application.Current as IApp).CurrentUser;
            if (currentUser?.Status == Statuses.Seller)
            {
                if(goodForSale.User.Id == currentUser.Id)
                {
                    menuItem = new MenuItem
                    {
                        Header = "Удалить товар",
                        Tag = goodForSale,
                    };
                    menuItem.Click += (_,_) => {
                        DeleteGoodItem(goodForSale);
                    };
                    image.ContextMenu.Items.Add(menuItem);
                }
                // TODO! доделать удаление карточки товара.
                
            }
           

        }

        private void OpenReview(object sender, RoutedEventArgs e)
        {

        }
    }
}
