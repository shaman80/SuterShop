using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuterShop.Chat
{
    internal partial class ChatViewModel : ObservableObject
    {
        [ObservableProperty] ObservableCollection<Message> messages;
        public ChatViewModel()
        {
            (Application.Current as IApp).MessageItemCountChanged += GoodMessageCountChanged;
        }

        private void GoodMessageCountChanged()
        {
            Application.Current.Dispatcher.Invoke(() => {
                SetData();
            });
        }

        public void SetData()
        {
            Messages = new ObservableCollection<Message> ((Application.Current as IApp).Db.Messages.Include("user").OrderBy(x => x.data).ToList());
        }

        internal void AddMessage(string text)
        {
                var date = DateTime.Now;
                Messages.Add(new Message { text = text, user = (Application.Current as IApp).CurrentUser, data = date});
                (Application.Current as IApp).Db.Messages.Add(new Message { text = text, user = (Application.Current as IApp).CurrentUser,data = date});
                (Application.Current as IApp).Db.SaveChanges(); 
        }
    }
}
