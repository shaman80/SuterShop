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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuterShop.Chat
{
    /// <summary>
    /// Логика взаимодействия для ChatView.xaml
    /// </summary>
    public partial class ChatView : UserControl
    {
        public ChatView()
        {
            InitializeComponent();
            scroll.ScrollToEnd();
        }

        private void SendMessage(object sender, RoutedEventArgs e)
        {
            (DataContext as ChatViewModel).AddMessage(BoxMessage.Text);
            BoxMessage.Text = "";
        }
   
        private void TextChanged(object sender, RoutedEventArgs e)
        {
            if( (Application.Current as IApp).CurrentUser != null)
            {

                MessageButton.IsEnabled = true; 
            }
            else
            {
                MessageButton.IsEnabled = false;
            }
        }

        private void BoxMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && MessageButton.IsEnabled) SendMessage(sender,e);
        }
    }
}
