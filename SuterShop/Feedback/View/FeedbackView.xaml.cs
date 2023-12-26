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

namespace SuterShop.Feedback
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

        private void SubmitReviewButton_Click(object sender, RoutedEventArgs e)
        {
            var db = (Application.Current as IApp).Db;

            var goods = db.GoodsForSaleList.ToList();

            db.Messages.Add(new Message
                {
                    Sender = (Application.Current as IApp).CurrentUser,
                    GoodsItem = goods.FirstOrDefault(),
                    MessageText = textBoxReview.Text,
                    DateAdded = DateTime.Now
                }
            ) ;
            db.SaveChanges();
            var messages = db.Messages.ToList();
            Close();
        }
    }
}
