using Microsoft.Win32;
using SuterShop.AdminPanel.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SuterShop.AdminPanel.View
{
    /// <summary>
    /// Логика взаимодействия для AdminPanelView.xaml
    /// </summary>
    public partial class AdminPanelView : Window
    {
        private GoodsForSale _goodItem;

        public AdminPanelView()
        {
            InitializeComponent();
            TestCurrentUser();
            LoadReport();
        }

        private void TestCurrentUser()
        {
            if((Application.Current as IApp).CurrentUser == null) return;
            if ((Application.Current as IApp).CurrentUser.Status == Statuses.Admin)
            {
                adminPanel.Visibility = Visibility.Visible;
                sellerPanel.Visibility = Visibility.Hidden;
                LoadSellers();
                LoadCategoryes();
            }else if ((Application.Current as IApp)!.CurrentUser.Status == Statuses.Seller)
            {
                adminPanel.Visibility = Visibility.Hidden;
                sellerPanel.Visibility = Visibility.Visible;
                LoadCategoryes();
            }
            else
            {
                adminPanel.Visibility = Visibility.Hidden;
                sellerPanel.Visibility = Visibility.Hidden;
            }
        }






        private void AddNewSeller(object sender, RoutedEventArgs e)
        {
            var seller = new User
            {
                FullName = foolName.Text,
                Login = login.Text,
                Email = email.Text,
                Password = password.Text,
                sum = 0,
                Status = Statuses.Seller,
            };

            foolName.Text = string.Empty;
            login.Text = string.Empty;
            email.Text = string.Empty;
            password.Text = string.Empty;

            (DataContext as AdminPanelViewModel).AddNewSeller(seller);
            LoadSellers();
        }

        private void LoadCategoryes()
        {
            (DataContext as AdminPanelViewModel).LoadCategoryes();
        }
        private void LoadReport()
        {
            (DataContext as AdminPanelViewModel).LoadReport();
        }

        private void LoadSellers()
        {
            (DataContext as AdminPanelViewModel).LoadSellers();
        }

        private void DeleteCategory(object sender, RoutedEventArgs e)
        {
            (sender as Button).IsEnabled = false;
            var category = (sender as Button).DataContext as Category;
            (DataContext as AdminPanelViewModel).DeleteCategory(category);
        }
        private void DeleteSeller(object sender, RoutedEventArgs e)
        {
            (sender as Button).IsEnabled=false;
            var seller = (sender as Button).DataContext as User;
            (DataContext as AdminPanelViewModel).DeleteSeller(seller);
        }
        private void AddNewCategory(object sender, RoutedEventArgs e)
        {
            (DataContext as AdminPanelViewModel).AddNewCategory(newCategotyTextBox.Text);
            newCategotyTextBox.Text = string.Empty;
        }

        private void SaveCategoryes(object sender, RoutedEventArgs e)
        {
            (DataContext as AdminPanelViewModel).SaveCategoryes();
        }

        private void AddNewOrEditGoodsItem(object sender, RoutedEventArgs e)
        {
            if(_goodItem != null)
            {
                EditGoodsItem();
            }
            else {
                AddNewGoodsItem();
            }       
        }

        private void EditGoodsItem()
        {
            int.TryParse(GoodsPrice.Text, out var price);
            int.TryParse(GoodsCount.Text, out var count);
            var category = categories.SelectedItem as Category;
            var butes = File.ReadAllBytes(Image.Text);

            _goodItem.Name = GoodsName.Text;
            _goodItem.Description = GoodsDescription.Text;
            _goodItem.Price = price;
            _goodItem.Count = count;
            _goodItem.Category = category;
            _goodItem.Image = butes;
            _goodItem.User = (Application.Current as IApp)!.CurrentUser;
   
            (DataContext as AdminPanelViewModel).EditGoodsItem(_goodItem);
            Close();
        }

        private void AddNewGoodsItem()
        {
            int.TryParse(GoodsPrice.Text, out var price);
            int.TryParse(GoodsCount.Text, out var count);
            var category = categories.SelectedItem as Category;
            var butes = File.ReadAllBytes(Image.Text);

            var goods = new GoodsForSale
            {
                Name = GoodsName.Text,
                Description = GoodsDescription.Text,
                Price = price,
                Count = count,
                Category = category,
                Image = butes,
                User = (Application.Current as IApp)!.CurrentUser
            };
            (DataContext as AdminPanelViewModel).AddNewGoodsItem(goods);
        }

        internal void OpenEditGoodsItem(GoodsForSale goodItem)
        {
            _goodItem = goodItem;
            AddOrEditBtn.Content = "Применить";
            GoodsName.Text = goodItem.Name;
            GoodsDescription.Text = goodItem.Description;
            GoodsPrice.Text = goodItem.Price.ToString();
            GoodsCount.Text = goodItem.Count.ToString();

            // Получаем папку куда пользователь установил нашу программу.
            var dir = $"{Directory.GetCurrentDirectory()}{System.IO.Path.DirectorySeparatorChar}TempImages{System.IO.Path.DirectorySeparatorChar}";
            var fileName = $"{goodItem.Id}_{goodItem.Name}.png";
            Image.Text = $"{dir}{fileName}";

            var cat = (categories.ItemsSource as ObservableCollection<Category>);
            for (int i = 0; i < cat.Count; i++)
            {
                var currentCategory = cat[i];
                if(goodItem.Category.Name == currentCategory.Name)
                {
                    categories.SelectedIndex = i;
                    break;
                }
            }
        }

        private void OpenFileDialogImage(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Картинка";
            if (openFileDialog.ShowDialog() == true)
            {
                Image.Text = openFileDialog.FileName;
            }
        }
        //zxc!!!

    }
}

