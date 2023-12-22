using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SuterShop.Feedback.View
{
    /// <summary>
    /// Логика взаимодействия для FeedbackView.xaml
    /// </summary>
    public partial class FeedbackView : Window
    {
        private User _user;
        private List<Message> _messages = new List<Message>();
        private DataBaseContext _db;
        private GoodsForSale _goodsItem;

        public FeedbackView()
        {
            InitializeComponent();

            _user = (Application.Current as IApp).CurrentUser;
            if(_user != null)
            {
                AddFeedbackBtn.Visibility = Visibility.Visible;
            }
            _db = (Application.Current as IApp).Db;
        }
               
        internal void SetData(GoodsForSale goodItem)
        {
            _goodsItem = goodItem;
            itemName.Content = goodItem.Name;
            itemCategory.Content = goodItem.Category.ToString();
            itemDescription.Content = goodItem.Description;

            _messages ??= new List<Message>();//ЕСЛИ messages = null, то этой строкой инициализируем этот массив
            _messages = _db.Messages.ToList();

            foreach (var message in _messages)
            {
                if (_goodsItem.Id == message.GoodsItem.Id)
                {
                    feedbackMessagesPanel.Children.Add(new TextBox { Text = message.MessageText });
                }

            }
        }

        private void AddFeedbackMessage(object sender, RoutedEventArgs e)
        {
            if (_user == null) return;
                
            var message = new Message
            {
                 MessageText = newFeedbackMessage.Text,
                 Sender = _user.Login,
                 GoodsItem = _goodsItem,
            };

            _db = (Application.Current as IApp).Db;
            _db.Messages.Add(message);
            _db.SaveChanges();
            feedbackMessagesPanel.Children.Add(new TextBox { Text = message.MessageText, Margin = new Thickness(5) });
            newFeedbackMessage.Text = String.Empty;
           
            
        }                     
    }
}

