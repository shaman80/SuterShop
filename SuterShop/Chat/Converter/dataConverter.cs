using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SuterShop.Chat.Converter
{
    internal class dataConverter : IValueConverter

    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime dateTime = (DateTime)value;
            string min = dateTime.Minute / 10 != 0 ? $"{dateTime.Minute}" : $"0{dateTime.Minute}";
            return $"{dateTime.Hour} : {min}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
