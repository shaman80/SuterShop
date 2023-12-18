using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using static SuterShop.App;

namespace SuterShop.AdminPanel.ViewModel
{
    internal partial class AdminPanelViewModel : ObservableObject
    {
        private DataBaseContext _db;
        [ObservableProperty] private User user;
        [ObservableProperty] private ObservableCollection<Category> categories;
        [ObservableProperty] private ObservableCollection<User> sellers;
        [ObservableProperty] private ObservableCollection<GoodsForSale> goodsForSale;
        private List<GoodsForSale> _goodsForSale;

        public AdminPanelViewModel()
        {
            _db = (Application.Current as IApp).Db;
            LoadGoods();
        }

        internal void LoadGoods()//загружаем в личный кабинет продавца, выводим список товаров
        {
            GoodsForSale = new ObservableCollection<GoodsForSale>();
            _goodsForSale = null;
            _goodsForSale = _db.GoodsForSaleList.ToList();
            foreach (var good in _goodsForSale)
            {
                if ((Application.Current as IApp)!.CurrentUser.Login != good.User.Login) continue;
                GoodsForSale.Add(good);
            }
        }

        internal void AddNewSeller(User seller)
        {
            _db.Users.Add(seller);
            _db.SaveChanges();
        }

        internal void LoadSellers()
        {
            Sellers = new ObservableCollection<User>();
            var listSellers = _db.Users.ToList();
            foreach (User seller in listSellers)
            {
                if (seller.Status != Statuses.Seller) continue;
                Sellers.Add(seller);
            }
        }

        internal void LoadCategoryes()
        {
            Categories = new ObservableCollection<Category>();
            var listCategory = _db.Category.ToList();
            foreach (var category in listCategory)
            {
                Categories.Add(category);
            }
        }

        internal void AddNewCategory(string categoryName)
        {
            if (categoryName == null || categoryName.Length < 3) return;
            var category = new Category
            {
                Name = categoryName
            };
            _db.Category.Add(category);
            _db.SaveChanges();
            LoadCategoryes();
        }

        internal void SaveCategoryes()
        {
            foreach(var category in categories)
            {
                _db.Category.Update(category);
            }
            _db.SaveChanges();
        }

        internal void DeleteCategory(Category? category)
        {
            _db.Category.Remove(category);
            _db.SaveChanges();
            LoadCategoryes();
        }

        internal void DeleteSeller(User? seller)
        {
            _db.Users.Remove(seller);
            _db.SaveChanges();
            LoadSellers();
        }

        internal void AddNewGoodsItem(GoodsForSale goods)
        {
            _db.GoodsForSaleList.Add(goods);
            _db.SaveChanges();
            LoadGoods();
        }

        private void GoodItemForSaleAdd(GoodsForSale goodsForSaleAdd)
        {
            GoodsForSale = null; 
            GoodsForSale.Add(goodsForSaleAdd);
        }
       
        internal void EditGoodsItem(GoodsForSale goods)
        {
            _db.GoodsForSaleList.Update(goods);
            _db.SaveChanges();
            (Application.Current as IApp).GoodItemChanged?.Invoke(goods);
           // LoadGoods();
        }

        internal void DeleteGoodItem(GoodsForSale? deleteGoodsItem)//Удаление товара в личном кабинете продавца
        {
            GoodsForSale.Remove(deleteGoodsItem);
            _db.GoodsForSaleList.Remove(deleteGoodsItem);
            _db.SaveChanges();
            LoadGoods();
        }
    }

}
