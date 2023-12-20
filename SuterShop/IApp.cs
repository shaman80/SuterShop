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
        public UpdateCardDelegate GoodItemDel { get; set; }
        public UpdateDelegate updateShopDelegate { get; set; }
        public UpdateDelegate updateChatDelegate { get; set; }
        void filterCentral(List<Category> categoriesFilter);
    }
}