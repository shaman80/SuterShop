using System.Collections.Generic;
using static SuterShop.App;

namespace SuterShop
{
    internal interface IApp
    {
        public User CurrentUser { get; set; }
        public DataBaseContext Db { get; set; }
        public UpdateUserDelegate UserIsLogining { get; set; }
        public UpdateCardDelegate GoodItemChanged { get; set; }
        public UpdateShopDelegate GoodItemCountChanged { get; set; }
        public СheckingLifeTimeDelegate СheckingLifeTime { get; set; }
        void filterCentral(List<Category> categoriesFilter);
    }
}