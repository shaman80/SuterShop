using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuterShop.card.viewModel
{
    internal partial class cardViewModel : ObservableObject
    {
        [ObservableProperty] private GoodsForSale good;
        private DataBaseContext _db;
        [ObservableProperty] private string imagePath;
        private List<GoodsForSale> _goods;

        public cardViewModel()
        {
            good = new GoodsForSale();
            _db = (Application.Current as IApp).Db;
            (Application.Current as IApp).GoodItemChanged += GoodIsChanged;
        }

        private void GoodIsChanged(GoodsForSale goodsForSale)
        {
            if (Good.Id != goodsForSale.Id) return;
            Good = null;
            Good = goodsForSale;
        }

        public void SetData(List<GoodsForSale> goods, GoodsForSale Goods, string iPath)
        {
            _goods = goods;
            ImagePath = iPath;
            Good = Goods;
        }

        internal void DeleteGoodItem(GoodsForSale goodForSale)
        {
            _goods.Remove(goodForSale);
            _db.SaveChanges();
        }
    }
}
