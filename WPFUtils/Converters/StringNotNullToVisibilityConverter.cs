using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFUtils.Converters
{
    /// <summary>
    /// Converts string is nul to visibility (Visible if string is not null and Collapsed otherwise)
    /// </summary>
    public class StringNotNullToVisibilityConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
