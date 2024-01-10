using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace SuterShop.Chat.Converter
{
    internal class converterOnlineUserColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            foreach(var item in (Application.Current as IApp).Db.OnlineUsers)
            {
                if (item.user?.Id.ToString() == value.ToString()) return "Lime";
            }
            return "red";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
