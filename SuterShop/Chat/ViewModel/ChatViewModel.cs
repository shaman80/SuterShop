using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
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
            chatMessages= new ObservableCollection<ChatMessage>(_db.ChatMessages);
            //RefreshChat();
        }
        

        public void SendMessage(ChatMessage newMessage)
        {
            var currentUser = (Application.Current as IApp).CurrentUser;
            if(currentUser == null)
            {
                newMessage.Sender = "Никто";
            }
            else
            {
                newMessage.Sender = currentUser.Login;
            }
            _db.ChatMessages.Add(newMessage);
            _db.SaveChanges();
            chatMessages.Add(newMessage);
            //RefreshChat();
        }

        public void RefreshChat()
        {       
            var messages = _db.ChatMessages.ToList();
            chatMessages = new ObservableCollection<ChatMessage>();
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
