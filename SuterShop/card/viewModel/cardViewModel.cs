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
        [ObservableProperty] private string imagePath;
        


        public cardViewModel()
        {
            good = new GoodsForSale();
            (Application.Current as IApp)!.GoodItemChanged += GoodIsChanged;
        }

        private void GoodIsChanged(GoodsForSale goodsForSale)
        {
            if (Good.Id != goodsForSale.Id) return;
            Good = null!;
            Good = goodsForSale;
        }

        public void SetData(GoodsForSale Goods, string iPath)
        {
            ImagePath = iPath;
            Good = Goods;
        }
    }
}
