using System;
using System.Globalization;
using System.Net;
using WordPressXF.Common;
using Xamarin.Forms;

namespace WordPressXF.Converters
{
    internal class HtmlStringToDecodedStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not string valueString)
                return string.Empty;

            return HtmlTools.Strip(WebUtility.HtmlDecode(valueString));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
