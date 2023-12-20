using SuterShop.Chat.viewModel;
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

namespace SuterShop.Chat.view
{
    /// <summary>
    /// Логика взаимодействия для chatView.xaml
    /// </summary>
    public partial class chatView : UserControl
    {
        public chatView()
        {
            InitializeComponent();
        }

        private void AddMessage(object sender, RoutedEventArgs e)
        {
           
            if ((Application.Current as IApp).CurrentUser != null)
            {
                ErrorUser.Visibility = Visibility.Hidden;
                (DataContext as chatViewModel).AddMessage(TextSelected.Text);
                TextSelected.Text = null;
            }
            else
            {
                ErrorUser.Visibility = Visibility.Visible;
            }
        }

        private void TextChanged(object sender, RoutedEventArgs e)
        {
            
                
                if (TextSelected.Text.Length > 0) Send.IsEnabled = true;
                else Send.IsEnabled = false;

        }
    }
}
