using CommunityToolkit.Mvvm.ComponentModel;
using SuterShop.Chat.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuterShop.Chat.ViewModel
{
    internal partial class ChatViewModel : ObservableObject
    {
        private DataBaseContext _db;
        [ObservableProperty] private ObservableCollection<ChatMessage> chatMessages;


        public ChatViewModel()
        {
            _db = (Application.Current as IApp).Db;
            RefreshChat();
        }

        public void SendMessage()
        {
            ChatMessages = new ObservableCollection<ChatMessage>();
            var listMessages= _db.ChatMessages.ToList();
            foreach (var message in listMessages)
            {
                listMessages.Add(message);
            }
            _db.SaveChanges();
            
        }
        public void RefreshChat()
        {
            var messages = _db.ChatMessages.ToList();
            if( messages.Count > 0 )
            {
                chatMessages.Clear();
            }        
            foreach (var message in messages)
            {
                ChatMessages.Add(message);
            }


        }

        internal void SetData(GoodsForSale goodItem)
        {
            
        }
    }
}
