using System;
using System.Globalization;
using WordPressPCL.Models;
using WordPressXF.Common;
using Xamarin.Forms;

namespace WordPressXF.Converters
{
    internal class HtmlStringToHtmlWebViewSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Post post)
            {
                var htmlContent = HtmlTools.CreateHtmlFromPost(post);
                return new HtmlWebViewSource { Html = htmlContent };
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
