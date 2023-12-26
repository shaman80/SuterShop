using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuterShop.Review
{
    internal partial class ReviewViewModel : ObservableObject


    {

        private DataBaseContext _db;


        [ObservableProperty] private ObservableCollection<Message> messages;

        private GoodsForSale _goodForSale;

        public ReviewViewModel()
        {
            _db = (Application.Current as IApp).Db;
        }

        internal void SetCurrentGood(GoodsForSale goodForSale)
        {
            _goodForSale = goodForSale;
            LoadMessages();

        }

        internal void LoadMessages()
        {
            
            Messages = new ObservableCollection<Message>();
            
            var listMessages = _db.Messages.ToList();

            foreach (Message message in listMessages)
            {
                if (message.GoodsItem.Id != _goodForSale.Id) continue;
                Messages.Add(message);
            }
        }

        
    }
}
