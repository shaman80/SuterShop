using SuterShop.LeftPanel.ViewModel;
using System.Windows;
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
        public Style style;

        private void Click(object sender, MouseButtonEventArgs e)
        {
            (DataContext as LeftPanelViewModel).Click(sender, e);
            if ((sender as TextBlock).Style == null)
            {
                (sender as TextBlock).Style = FindResource("HighlightStyle") as Style;
            }
            else
            {

                (sender as TextBlock).Style = null;
            }
        }
    }
}
