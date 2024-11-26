using BreadGPT.Models;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace BreadGPT.Styles
{
    class MessageBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MessageSender.User)
            {
                return Brushes.LightBlue;
            }
            else if(value is MessageSender.ChatGPT)
            {
                return Brushes.LightGray;
            }

            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
