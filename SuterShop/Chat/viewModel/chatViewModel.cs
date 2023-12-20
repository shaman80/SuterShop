using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SuterShop.Chat.viewModel
{
    internal partial class chatViewModel : ObservableObject
    {
        public chatViewModel()
        {
            (Application.Current as IApp).updateChatDelegate += GoodMessageCountChanged;
        }
        [ObservableProperty] ObservableCollection<Message> messages;

        private void GoodMessageCountChanged()
        {
            Application.Current.Dispatcher.Invoke(() => SetData());
        }
        public void SetData()
        {
            Messages = new ObservableCollection<Message>((Application.Current as IApp).Db.Messages.Include("user").OrderBy(x => x.Data).ToList());


        }

        internal void AddMessage(string textSelected)
        {
            var now = DateTime.Now;
            Messages.Add(new Message { text = textSelected, user = (Application.Current as IApp).CurrentUser, Data =  now});
            (Application.Current as IApp).Db.Messages.Add(new Message {text = textSelected,user = (Application.Current as IApp).CurrentUser, Data = now });
            (Application.Current as IApp).Db.SaveChanges();
        }
    }
}
