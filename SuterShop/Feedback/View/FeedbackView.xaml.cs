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
        public FeedbackView()
        {
            InitializeComponent();
        }
  
        
        internal void SetData(GoodsForSale goodItem)
        {
            //var dir = $"{Directory.GetCurrentDirectory()}{System.IO.Path.DirectorySeparatorChar}TempImages{System.IO.Path.DirectorySeparatorChar}";
            //var fileName = $"{goodItem.Category.Id}_{goodItem.Id}.png";
            //if (!File.Exists($"{dir}{fileName}"))
            //{
            //    File.WriteAllBytes($"{dir}{fileName}", goodItem.Image);
            //}
            itemName.Content = goodItem.Name;
            itemCategory.Content = goodItem.Category;
            itemDescription.Content = goodItem.Description;
            //itemImage.Source = new BitmapImage(new Uri(fileName));
        }

        private void AddFeedbackMessage(object sender, RoutedEventArgs e)
        {
            var message = new Message
            {
                messageText = newFeedbackMessage.Text,
            };
            var db = (Application.Current as IApp).Db;
            db.Messages.Add(message);
            db.SaveChanges();
            feedbackMessagesPanel.Children.Add(new TextBox { Text = message.MessageText });
        }
    }
}
