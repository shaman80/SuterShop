using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuterShop.GoodInfo
{
    public partial class GoodInfoControlViewModel : ObservableObject
    {
        [ObservableProperty] private Goods good;

        public GoodInfoControlViewModel()
        {

        }
        public GoodInfoControlViewModel(Goods goods)
        {
            good = goods;
        }
    }
}
