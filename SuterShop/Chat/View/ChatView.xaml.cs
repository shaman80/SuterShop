using SuterShop.Chat.ViewModel;
using System;
using System.Collections.Generic;
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

namespace SuterShop.Chat.View
{
    /// <summary>
    /// Логика взаимодействия для ChatView.xaml
    /// </summary>
    public partial class ChatView : Window
    {
        public ChatView()
        {
            InitializeComponent();
            (DataContext as ChatViewModel).SetData(StackPanel);
            //(DataContext as ChatViewModel).LoadChatMessages();
        }

        private void SendChatMessage(object sender, RoutedEventArgs e)
        {
            var chatTextMessage = ChatTextBox.Text;
            //var text = new TextBlock { Text = chatTextMessage };
            //StackPanel.Children.Add(text);
           
            (DataContext as ChatViewModel).SendChatMessage(chatTextMessage);
            ChatTextBox.Text = string.Empty;
        }
    }
}
