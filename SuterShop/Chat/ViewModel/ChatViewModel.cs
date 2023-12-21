using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SuterShop.Chat.ViewModel
{
    class ChatViewModel
    {
        private DataBaseContext _db;
        private Goods Good { get; set; }
        private StackPanel StackPanel {  get; set; }

        private List<ChatMessage> _chatMessage;

        private int _countMessage;

        private string _cs = "Server=localhost;Database=shop;Uid=root;Pwd=1q2w3e;";

        private Timer _timer;
        public ChatViewModel()
        {
            _db = (Application.Current as IApp).Db;

            Thread.Sleep(1000);
            _timer = new Timer(TimerTick, null, 0, 1000);

            (Application.Current as IApp).ChatMessageCount += ChatMessageCount;
            // LoadChatMessages();
        }

        private void ChatMessageCount()
        {
            Application.Current.Dispatcher.Invoke(() => {
                LoadChatMessages();
            });
        }
        internal void SendChatMessage(string chatTextMessage)
        {
            var chatMessage = new ChatMessage() 
            { 
               Message = chatTextMessage,
               User = (Application.Current as IApp).CurrentUser,
               Seller = Good.User,
               GoodItem = Good,
               SendUserIdMessage = (Application.Current as IApp).CurrentUser.Id
            };
            _db.ChatMessages.Add(chatMessage);
            _db.SaveChanges();
           
        }

        internal void SetGoodsItem(Goods? goodsItem)
        {
            Good = goodsItem;
            LoadChatMessages();
        }
        internal void LoadChatMessages()
        {
            var _app = Application.Current as IApp;
            _chatMessage = null;
            _chatMessage = _db.ChatMessages.ToList();
            StackPanel.Children.Clear();
            foreach (var chatMessage in _chatMessage)
            {
                if(((chatMessage.User.Id == _app.CurrentUser.Id) || (chatMessage.Seller.Id == _app.CurrentUser.Id)) && (chatMessage.GoodItem.Id == Good.Id) ) 
                {
                    var _textBlock = new TextBlock() { Text = chatMessage.Message };
                    if(chatMessage.SendUserIdMessage == _app.CurrentUser.Id)
                    {
                        _textBlock.HorizontalAlignment = HorizontalAlignment.Right;
                    }
                    StackPanel.Children.Add(_textBlock);
                }

            }
        }

        internal void SetData(StackPanel stackPanel)
        {
            StackPanel = stackPanel;
        }

        private void TimerTick(object? state)
        {
            var db = new DataBaseContext(_cs);
            var countMessage = db.ChatMessages.Count();
            db.Database.CloseConnection();

            if (_countMessage != countMessage)
            {
                (Application.Current as IApp).ChatMessageCount?.Invoke();
            };
            _countMessage = countMessage;
        }
    }
}
