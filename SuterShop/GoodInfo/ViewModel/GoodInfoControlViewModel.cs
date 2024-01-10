using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuterShop.GoodInfo
{
    public partial class GoodInfoWindowViewModel : ObservableObject
    {
        [ObservableProperty] private Goods good;

        public GoodInfoWindowViewModel()
        {

        }
        
        public void SetData(Goods g)
        {
            Good = g;
        }
    }
}
