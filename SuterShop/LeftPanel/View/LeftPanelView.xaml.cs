using SuterShop.LeftPanel.ViewModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SuterShop.LeftPanel.View
{
    /// <summary>
    /// Логика взаимодействия для LeftPanelView.xaml
    /// </summary>
    public partial class LeftPanelView : UserControl
    {
        public LeftPanelView()
        {
            InitializeComponent();
        }



        private void Click(object sender, MouseButtonEventArgs e)
        {
            var bc = new BrushConverter();

            Brush n = (Brush)bc.ConvertFrom("#FFFFFF");

            if ((sender as TextBlock).Background.ToString() == n.ToString()) (sender as TextBlock).Background = (Brush)bc.ConvertFrom("#343E40");
            else (sender as TextBlock).Background = n;
            (DataContext as LeftPanelViewModel).Click(sender, e);

        }
    }
}
