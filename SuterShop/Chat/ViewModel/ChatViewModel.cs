using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SuterShop.Chat.ViewModel
{
    class ChatViewModel
    {
        private DataBaseContext _db;
        private Goods Good { get; set; }
        private StackPanel StackPanel {  get; set; }

        private List<ChatMessage> _chatMessage;

        private int _countMessage;

        private string _cs = "Server=localhost;Database=shopD;Uid=root;Pwd=1q2w3e;";
        //private string _cs = "Server=192.168.88.54;Database=shopD;Uid=root;Pwd=1q2w3e;";

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
                SendUserIdMessage = (Application.Current as IApp).CurrentUser,
                Time = DateTime.Now 
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
                if (_app.CurrentUser.Id == chatMessage.Seller.Id)
                {
                    if ((chatMessage.Seller.Id == _app.CurrentUser.Id) && (chatMessage.GoodItem.Id == Good.Id))
                    {
                        var _textBlockLogin = new TextBlock() { Text = chatMessage.SendUserIdMessage.Login + "   " + (chatMessage.Time.ToShortTimeString()).ToString(), HorizontalAlignment = HorizontalAlignment.Right };
                        var _textBlock = new TextBlock() { Text = chatMessage.Message };
                        var _stackPanel = new StackPanel() { Margin = new Thickness(10) };
                        var _bordre = new Border() { BorderBrush = new SolidColorBrush(Color.FromRgb(171, 204, 207)), BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(10), Background = new SolidColorBrush(Color.FromRgb(171, 204, 207)) };
                        _bordre.HorizontalAlignment = HorizontalAlignment.Left;
                        if (chatMessage.SendUserIdMessage.Id == _app.CurrentUser.Id)
                        {
                            _bordre.HorizontalAlignment = HorizontalAlignment.Right;
                            _bordre.Background = new SolidColorBrush(Color.FromRgb(66, 224, 245));
                            _bordre.BorderBrush = new SolidColorBrush(Color.FromRgb(66, 224, 245));
                        }
                        _stackPanel.Children.Add(_textBlockLogin);
                        _stackPanel.Children.Add(_textBlock);
                        _bordre.Child = _stackPanel;
                        StackPanel.Children.Add(_bordre);
                    }
                }  
                else if (chatMessage.GoodItem.Id == Good.Id)
                {
                    var _textBlockLogin = new TextBlock() { Text = chatMessage.SendUserIdMessage.Login + "   " + (chatMessage.Time.ToShortTimeString()).ToString(), HorizontalAlignment = HorizontalAlignment.Right };
                    var _textBlock = new TextBlock() { Text = chatMessage.Message };
                    var _stackPanel = new StackPanel() { Margin = new Thickness(10) };
                    var _bordre = new Border() { BorderBrush = new SolidColorBrush(Color.FromRgb(171, 204, 207)), BorderThickness = new Thickness(1), CornerRadius = new CornerRadius(10), Background = new SolidColorBrush(Color.FromRgb(171, 204, 207)) };
                    _bordre.HorizontalAlignment = HorizontalAlignment.Left;
                    if (chatMessage.SendUserIdMessage.Id == _app.CurrentUser.Id)
                    {
                        _bordre.HorizontalAlignment = HorizontalAlignment.Right;
                        _bordre.Background = new SolidColorBrush(Color.FromRgb(66, 224, 245));
                        _bordre.BorderBrush = new SolidColorBrush(Color.FromRgb(66, 224, 245));
                    }
                    _stackPanel.Children.Add(_textBlockLogin);
                    _stackPanel.Children.Add(_textBlock);
                    _bordre.Child = _stackPanel;
                    StackPanel.Children.Add(_bordre);
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
